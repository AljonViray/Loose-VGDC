using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trebuchet : MonoBehaviour {

    GameObject loadedAmmo;

    public bool isArmed;

	// Use this for initialization
	void Start () {
        isArmed = true;
    }
	
	// Update is called once per frame
	void Update () {

		if(this.gameObject.transform.GetChild(0).transform.GetChild(1).childCount > 0)
        {
            loadedAmmo = this.gameObject.transform.GetChild(0).transform.GetChild(1).transform.GetChild(0).gameObject;
        }
        else
        {
            loadedAmmo = null;
        }
	}

    public void Loose() {
        if(loadedAmmo != null)
        {
            isArmed = false;
            this.gameObject.transform.GetChild(0).transform.Rotate(0, 0, -45f);
            //loadedAmmo.transform.localPosition = loadedAmmo.transform.localPosition + new Vector3(0, 2, 0);
            loadedAmmo.transform.parent = null;
            loadedAmmo.GetComponent<Rigidbody>().isKinematic = false;
            loadedAmmo.GetComponent<Rigidbody>().velocity
                = this.gameObject.transform.GetChild(0).transform.GetChild(1).transform.up * 60;
            this.gameObject.transform.GetChild(0).transform.Rotate(0, 0, -45f);
            loadedAmmo = null;
        }

    }

    public void Arm()
    {
        if(isArmed == false)
        {
            this.gameObject.transform.GetChild(0).transform.Rotate(0, 0, 90f);
            isArmed = true;
        }


    }
}
