using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private GameObject lookingAtObject;
    private Ray lineOfSightRay;
    public RaycastHit hit;

    private GameObject catapult;
    public float maxAngle, minAngle;
    public float maxPower, minPower;

    private GameObject activeCam;
    private GameObject newPC;
    private GameObject currentCarriedObject;

    private GameObject castleArea;
    private Vector3 center;
    private Vector3 size;

    private GameObject ammoSpawn;
    public GameObject rockPrefab;
    public int maxAmmo;

    public bool isGrabbingCatapult;

    public float rotatingCatpultSpeed;

    //private float firstFloat;
    //private float secondFloat;

    //private bool firstPass;



    void Start ()
    {
        catapult = GameObject.Find("Catapult");
        isGrabbingCatapult = false;
        currentCarriedObject = null;
        activeCam = this.gameObject.transform.GetChild(0).gameObject;
        //firstFloat = 89f;
        //secondFloat = 70f;
        //firstPass = true;
    }


    void Update ()
    {
        if(isGrabbingCatapult == false)
        {
            lookingAtObject = closestObject(3);
        }


        //Switch Player Characters
        if (Input.GetKeyDown(KeyCode.Tab) && this.gameObject.GetComponent<MovementAndLook>().enabled == true)
        {
            if (this.gameObject.name == "Player_Loader")
            {
                newPC = GameObject.Find("Player_Scout");
            }
            else
            {
                newPC = GameObject.Find("Player_Loader");
            }

            this.gameObject.GetComponent<MovementAndLook>().enabled = false;
            this.gameObject.GetComponent<Interaction>().enabled = false;

            newPC.GetComponent<MovementAndLook>().enabled = true;
            newPC.GetComponent<Interaction>().enabled = true;

            GameObject cam = GameObject.Find("Main Camera");
            Vector3 localPos = cam.transform.localPosition;
            cam.transform.SetParent(newPC.transform);
            cam.transform.localPosition = localPos;
            cam.transform.localRotation = Quaternion.Euler(0, 0, 0);

            Debug.Log(this.gameObject);
            Debug.Log(this.transform.childCount);
            if(this.transform.childCount > 0)
            {
                activeCam = this.gameObject.transform.GetChild(0).gameObject;
            }
        }


        if (lookingAtObject != null)
        {
            //Debug.Log(lookingAtObject);
            //Picking up Ammo objects
            if (lookingAtObject.tag == "Ammo" && currentCarriedObject == null && Input.GetKeyDown(KeyCode.E))
            {
                currentCarriedObject = lookingAtObject;
                currentCarriedObject.GetComponent<Rigidbody>().isKinematic = true;
                currentCarriedObject.transform.SetParent(this.gameObject.transform);
            }

            else if (lookingAtObject.tag == "Ammo" && Input.GetKeyDown(KeyCode.F))
            {
                lookingAtObject.transform.GetChild(0).gameObject.SetActive(!lookingAtObject.transform.GetChild(0).gameObject.activeSelf);
            }

            else if (currentCarriedObject != null && Input.GetKeyDown(KeyCode.E))
            {
                currentCarriedObject.transform.parent = null;
                currentCarriedObject.GetComponent<Rigidbody>().isKinematic = false;
                currentCarriedObject = null;
            }

         /////////////////////////////////////////////////////////////////

            //Interacting with buttons, etc.
            if (lookingAtObject.CompareTag("Loose"))
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    catapult.GetComponent<Catapult>().Loose();
                    foreach(Animation a in hit.collider.gameObject.GetComponentsInChildren<Animation>())
                    {
                        a.Play();
                    }
                }

            }

            else if (lookingAtObject.CompareTag("Reload"))
            {
                //Debug.Log(catapult.transform.GetChild(0).GetChild(0).transform.eulerAngles.x);

                if (catapult.GetComponent<Catapult>().isArmed == false)
                {
                    catapult.transform.GetChild(0).GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
                    //Debug.Log(catapult.transform.GetChild(0).GetChild(0).gameObject + " " + catapult.transform.GetChild(0).GetChild(0).transform.localEulerAngles);

                    if (Input.GetKey(KeyCode.E))
                    {

                        catapult.transform.GetChild(0).GetChild(0).transform.Rotate(1, 0, 0);
                        hit.collider.gameObject.GetComponentInChildren<Animation>().Play();

                        //if(firstPass)
                        //{
                        //    if(catapult.transform.GetChild(0).GetChild(0).transform.localRotation.eulerAngles.x < firstFloat)
                        //    {
                        //        Debug.Log("TRUE < FIRST");
                        //        catapult.transform.GetChild(0).GetChild(0).transform.Rotate(1, 0, 0);
                        //        hit.collider.gameObject.GetComponentInChildren<Animation>().Play();
                        //        Debug.Log(catapult.transform.GetChild(0).GetChild(0).transform.localRotation.eulerAngles.x);

                        //    }
                        //    else
                        //    {
                        //        Debug.Log("TRUE > FIRST");
                        //        catapult.transform.GetChild(0).GetChild(0).transform.Rotate(1, 0, 0);
                        //        Debug.Log(catapult.transform.GetChild(0).GetChild(0).transform.localRotation.eulerAngles.x);

                        //        firstPass = false;
                        //        //UnityEditor.EditorApplication.isPaused = true;
                        //    }
                            
                        //}
                        //else
                        //{
                        //    if (catapult.transform.GetChild(0).GetChild(0).transform.localRotation.eulerAngles.x > secondFloat)
                        //    {
                        //        Debug.Log("FALSE > SECOND");
                        //        catapult.transform.GetChild(0).GetChild(0).transform.Rotate(1, 0, 0);
                        //        hit.collider.gameObject.GetComponentInChildren<Animation>().Play();

                        //    }
                        //    else
                        //    {
                        //        Debug.Log("TRUE < SECOND");
                        //        catapult.GetComponent<Catapult>().Arm();
                        //        catapult.transform.GetChild(0).GetChild(0).transform.localRotation = Quaternion.Euler(70, 180, 180);
                        //        hit.collider.gameObject.GetComponentInChildren<Animation>().Stop();

                        //        firstPass = true;
                        //    }
                        //}

                    }

                }
                if (Input.GetKeyUp(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponentInChildren<Animation>().Stop();
                }
            }

            else if (lookingAtObject.CompareTag("StopBarRotate"))
            {
                GameObject bar = GameObject.Find("StopBar").transform.GetChild(0).gameObject;
                //Debug.Log(bar.transform.rotation.x);

                if (Input.GetKey(KeyCode.E) && bar.transform.rotation.x <= maxAngle)
                {

                    lookingAtObject.GetComponentInChildren<Animator>().SetBool("Forward", false);
                    lookingAtObject.gameObject.GetComponentInChildren<Animator>().SetBool("Backward", true);
                    bar.transform.Rotate(.5f, 0, 0);
                }
                else if (Input.GetKey(KeyCode.Q) && bar.transform.rotation.x >= minAngle)
                {
                    lookingAtObject.gameObject.GetComponentInChildren<Animator>().SetBool("Backward", false);
                    lookingAtObject.gameObject.GetComponentInChildren<Animator>().SetBool("Forward", true);

                    bar.transform.Rotate(-.5f, 0, 0);
                }
                else
                {
                    lookingAtObject.gameObject.GetComponentInChildren<Animator>().SetBool("Forward", false);
                    lookingAtObject.gameObject.GetComponentInChildren<Animator>().SetBool("Backward", false);
                }
            }

            else if (lookingAtObject.CompareTag("CatapultRotate"))
            {
                if (Input.GetKey(KeyCode.E) && isGrabbingCatapult == false)
                {
                    isGrabbingCatapult = true;
                    this.gameObject.transform.SetParent(lookingAtObject.transform.parent);
                    //catapult.transform.GetChild(0).Rotate(0, .5f, 0);
                }
                else if (Input.GetKey(KeyCode.E) && isGrabbingCatapult == true)
                {
                    isGrabbingCatapult = false;
                    this.gameObject.transform.parent = null;
                    //catapult.transform.GetChild(0).Rotate(0, -.5f, 0);
                }
            }

            else if (lookingAtObject.name == "SpawnEnemy" && Input.GetKeyDown(KeyCode.E))
            {
                GameObject.Find("EnemyController").GetComponent<EnemyController>().spawnSiegeTower();
            }

            else if (lookingAtObject.tag == "Storage" && Input.GetKeyDown(KeyCode.E))
            {
                if (GameObject.FindGameObjectsWithTag("Ammo").Length < maxAmmo)
                {
                    Vector3 adjustedPos = lookingAtObject.transform.position + new Vector3(0, 2, 0);
                    Instantiate(rockPrefab, adjustedPos, Quaternion.identity);
                }
            }
        }
    }


    GameObject closestObject(float maxRange)
    {
        if (Physics.Raycast(activeCam.transform.position, activeCam.transform.TransformDirection(Vector3.forward), out hit, maxRange))
        {
            
            Debug.DrawRay(activeCam.transform.position, activeCam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }
}