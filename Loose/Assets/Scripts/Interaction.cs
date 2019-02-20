using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private GameObject lookingAtObject;
    private Ray lineOfSightRay;
    public RaycastHit hit;

    public GameObject catapult;
    public float maxAngle;
    public float minAngle;

    private GameObject activeCam;
    public GameObject newPC;
    public GameObject currentCarriedObject;

    private Vector3 center;
    private Vector3 size;
    public GameObject castleArea;

    private GameObject ammoSpawn;
    public GameObject rockPrefab;
    public float amountToSpawn;
    public int maxAmmo;


    void Start ()
    {
        catapult = GameObject.Find("Catapult");

        currentCarriedObject = null;
        activeCam = this.gameObject.transform.GetChild(0).gameObject;
    }


    void Update ()
    {
        lookingAtObject = closestObject(5);

        //Switch Player Characters
        if (Input.GetKeyDown(KeyCode.Tab))
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

            activeCam = this.gameObject.transform.GetChild(0).gameObject;
        }


        //Pick up Ammo objects
        if (lookingAtObject != null)
        {
            if (lookingAtObject.tag == "Ammo" && currentCarriedObject == null && Input.GetKey(KeyCode.E))
            {
                currentCarriedObject = lookingAtObject;
                currentCarriedObject.GetComponent<Rigidbody>().isKinematic = true;
                currentCarriedObject.transform.SetParent(this.gameObject.transform);
            }
            else if (currentCarriedObject != null && Input.GetKey(KeyCode.E))
            {
                currentCarriedObject.transform.parent = null;
                currentCarriedObject.GetComponent<Rigidbody>().isKinematic = false;
                currentCarriedObject = null;
            }
        }


        //Interact with buttons, etc.
        if (lookingAtObject != null)
        {
            if (lookingAtObject.name == "Loose")
            {
                if (Input.GetKeyDown(KeyCode.E) && catapult.GetComponent<Catapult>().isArmed)
                {
                    catapult.GetComponent<Catapult>().Loose();
                }
                else if (Input.GetKeyDown(KeyCode.E) && !catapult.GetComponent<Catapult>().isArmed)
                {
                    catapult.GetComponent<Catapult>().Arm();
                }
            }

            if (lookingAtObject.name == "TurnCatapult")
            {
                if (Input.GetKey(KeyCode.E))
                {
                    catapult.transform.Rotate(0, .5f, 0);
                }
                else if (Input.GetKey(KeyCode.Q))
                {
                    catapult.transform.Rotate(0, -.5f, 0);
                }
            }

            else if (lookingAtObject.name == "ChangeAngle")
            {
                GameObject bar = GameObject.Find("StopBar");

                if (Input.GetKey(KeyCode.E) && bar.transform.rotation.x <= maxAngle)
                {
                    bar.transform.Rotate(.5f, 0, 0);
                }
                else if (Input.GetKey(KeyCode.Q) && bar.transform.rotation.x >= minAngle)
                {
                    bar.transform.Rotate(-.5f, 0, 0);
                }

                Debug.Log(bar.transform.rotation);
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
            return hit.transform.gameObject;
        }
        else
        {
            return null;
        }
    }
}