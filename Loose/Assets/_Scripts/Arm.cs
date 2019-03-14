using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour {

    public float xTorque;

    public BoxCollider[] bucketColliders;

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Rigidbody>().centerOfMass = this.gameObject.transform.localPosition;
        LowerBucketEdges();
        //this.gameObject.GetComponent<Rigidbody>().AddRelativeTorque(xTorque,0,0,ForceMode.VelocityChange);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LowerBucketEdges()
    {
        for(int i = 0; i < 4; ++i)
        {
            bucketColliders[i].size = new Vector3(bucketColliders[i].size.x, bucketColliders[i].size.y, bucketColliders[i].size.z * .5f);
        }
    }

    public void RaiseBucketEdges()
    {
        for (int i = 0; i < 4; ++i)
        {
            bucketColliders[i].size = new Vector3(bucketColliders[i].size.x, bucketColliders[i].size.y, bucketColliders[i].size.z * 2f);
        }
    }
}
