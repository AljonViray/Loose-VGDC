using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

    public float xTorque;

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Rigidbody>().centerOfMass = this.gameObject.transform.localPosition;
        //this.gameObject.GetComponent<Rigidbody>().AddRelativeTorque(xTorque,0,0,ForceMode.VelocityChange);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
