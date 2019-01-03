using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {


    private GameObject lookingAtObject;
    private Ray lineOfSightRay;
    private GameObject activeCam;
    public GameObject currentCarriedObject;
	// Use this for initialization
	void Start () {
        currentCarriedObject = null;
        activeCam = this.gameObject.transform.GetChild(0).gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        lookingAtObject = closestObject(3);


        if (Input.GetKeyDown(KeyCode.E))
        {
            if(lookingAtObject == null)
            {
                if(currentCarriedObject != null)
                {
                    currentCarriedObject.transform.parent = null;
                    currentCarriedObject = null; 
                }
            }
            else
            {
                if( lookingAtObject.tag == "Ammo")
                {
                    if (currentCarriedObject == null)
                    {
                        currentCarriedObject = lookingAtObject;
                        currentCarriedObject.transform.SetParent(this.gameObject.transform);
                    }
                }
                else if (lookingAtObject.tag == "Container")
                {    
                    if (currentCarriedObject != null)
                    {

                        currentCarriedObject.transform.SetParent(lookingAtObject.transform);

                        currentCarriedObject.transform.localPosition = new Vector3(0, 1.3f, 0);

                        currentCarriedObject = null;
                    }
                }

                if (lookingAtObject.name == "Loose" )
                {
                    if(GameObject.Find("Treb").GetComponent<Trebuchet>().isArmed)
                    {
                        GameObject.Find("Treb").GetComponent<Trebuchet>().Loose();
                    }
                    else
                    {
                        GameObject.Find("Treb").GetComponent<Trebuchet>().Arm();
                    }

                }


            }


        }

        if(lookingAtObject != null)
        {
            if (lookingAtObject.name == "TurnCW" && Input.GetKey(KeyCode.E))
            {
                GameObject.Find("Treb").transform.Rotate(0, -.5f, 0);
            }
            if (lookingAtObject.name == "TurnCCW" && Input.GetKey(KeyCode.E))
            {
                GameObject.Find("Treb").transform.Rotate(0, .5f, 0);
            }
            if(lookingAtObject.name == "SpawnEnemy" && Input.GetKeyDown(KeyCode.E))
            {
                GameObject.Find("EnemyController").GetComponent<EnemyController>().spawnSiegeTower();
            }
        }

    }

    GameObject closestObject(float maxRange) {
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
