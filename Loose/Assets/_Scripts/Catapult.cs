using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour {

    GameObject loadedAmmo;

    public bool isArmed;

    Quaternion armedRotation;

	// Use this for initialization
	void Start () {
        isArmed = true;
        armedRotation = this.transform.GetChild(0).GetChild(0).transform.rotation;
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void Loose() {
        if(isArmed)
        {
            this.transform.GetChild(0).GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            isArmed = false;
        }

    }

    public void Arm()
    {
        if(isArmed == false)
        {
            this.transform.GetChild(0).GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            this.transform.GetChild(0).GetChild(0).transform.Rotate(110 - this.transform.GetChild(0).GetChild(0).transform.rotation.eulerAngles.x,0,0);
            isArmed = true;
        }


    }
}
