using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeTower : MonoBehaviour {


    public float moveSpeed;
    Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = this.gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rb.MovePosition(this.gameObject.transform.position + this.gameObject.transform.forward * moveSpeed * Time.deltaTime);
	}
}
