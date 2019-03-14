using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{

    public float xTorque;

    void Start ()
    {
        //this.gameObject.GetComponent<Rigidbody>().AddRelativeTorque(xTorque,0,0,ForceMode.VelocityChange);
	}

    private void Update()
    {
        this.gameObject.GetComponent<Rigidbody>().centerOfMass = this.gameObject.transform.localPosition;

    }

}
