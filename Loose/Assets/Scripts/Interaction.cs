using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private GameObject lookingAtObject;
    private Ray lineOfSightRay;
    private GameObject activeCam;
    public GameObject currentCarriedObject;

    public GameObject castleArea;
    private Vector3 center;
    private Vector3 size;
    public float amountToSpawn;
    public GameObject rockPrefab;


    void Start ()
    {
        currentCarriedObject = null;
        activeCam = this.gameObject.transform.GetChild(0).gameObject;
    } 


    void Update ()
    {
        lookingAtObject = closestObject(3);

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            GameObject newPC;
            if(this.gameObject.name == "Player_Loader")
            {
                newPC = GameObject.Find("Player_Scout");
            }
            else
            {
                newPC = GameObject.Find("Player_Loader");
            }

            this.gameObject.GetComponent<MovementAndLook>().enabled = false;
            newPC.GetComponent<MovementAndLook>().enabled = true;

            this.gameObject.GetComponent<Interaction>().enabled = false;
            newPC.GetComponent<Interaction>().enabled = true;

            GameObject cam = GameObject.Find("Main Camera");
            Vector3 localPos = cam.transform.localPosition;
            cam.transform.SetParent(newPC.transform);
            cam.transform.localPosition = localPos;
            cam.transform.localRotation = Quaternion.Euler(0, 0, 0);

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(lookingAtObject == null && currentCarriedObject != null)
            {
                currentCarriedObject.transform.parent = null;
                currentCarriedObject.GetComponent<Rigidbody>().isKinematic = false;
                currentCarriedObject = null; 
            }

            else
            {
                if( lookingAtObject.tag == "Ammo" && currentCarriedObject == null)
                {
                    currentCarriedObject = lookingAtObject;
                    currentCarriedObject.GetComponent<Rigidbody>().isKinematic = true;
                    currentCarriedObject.transform.SetParent(this.gameObject.transform);
                }
                else if (lookingAtObject.tag == "Container" && currentCarriedObject != null)
                {    
                    currentCarriedObject.transform.SetParent(lookingAtObject.transform);

                    currentCarriedObject.transform.localPosition = new Vector3(0, 1.3f, 0);

                    currentCarriedObject = null;
                }

                if (lookingAtObject.name == "Loose" )
                {
                    if(GameObject.Find("Catapult").GetComponent<Catapult>().isArmed)
                    {
                        GameObject.Find("Catapult").GetComponent<Catapult>().Loose();
                    }
                    else
                    {
                        GameObject.Find("Catapult").GetComponent<Catapult>().Arm();
                    }

                }


            }


        }

        if(lookingAtObject != null)
        {
            if (lookingAtObject.name == "TurnCW" && Input.GetKey(KeyCode.E))
            {
                GameObject.Find("Catapult").transform.Rotate(0, -.5f, 0);
            }
            else if (lookingAtObject.name == "TurnCCW" && Input.GetKey(KeyCode.E))
            {
                GameObject.Find("Catapult").transform.Rotate(0, .5f, 0);
            }
            else if (lookingAtObject.name == "ReleaseSooner" && Input.GetKey(KeyCode.E))
            {
                GameObject bar = GameObject.Find("StopBar");
                bar.transform.Rotate(-.5f,0, 0);
                Debug.Log(bar.transform.eulerAngles);
            }
            else if (lookingAtObject.name == "ReleaseLater" && Input.GetKey(KeyCode.E))
            {
                GameObject bar = GameObject.Find("StopBar");
                bar.transform.Rotate(.5f, 0, 0);
                Debug.Log(bar.transform.eulerAngles);
            }
            else if (lookingAtObject.name == "SpawnEnemy" && Input.GetKeyDown(KeyCode.E))
            {
                GameObject.Find("EnemyController").GetComponent<EnemyController>().spawnSiegeTower();
            }
            else if (lookingAtObject.name == "SpawnRocks" && Input.GetKeyDown(KeyCode.E))
            {
                castleArea = GameObject.Find("CastleArea");
                center = castleArea.transform.position;
                size = castleArea.transform.localScale;

                for (int i=0; i < amountToSpawn; i++)
                {
                    Vector3 spawnPoint = center + new Vector3
                        (Random.Range(-size.x/2, size.x/2), 0, Random.Range(-size.z / 2, size.z / 2));

                    Instantiate(rockPrefab, spawnPoint, Quaternion.identity);
                }
            }

        }

    }

    GameObject closestObject(float maxRange)
    {
        RaycastHit hit;
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
