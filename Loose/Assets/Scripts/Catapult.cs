using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    GameObject loadedAmmo;
    public bool isArmed;
    public bool wasFired;
    float armedRotation;


	void Start()
    {
        isArmed = true;
        wasFired = false;
        armedRotation = this.transform.GetChild(0).GetChild(0).transform.rotation.x;
    }


    public void Loose()
    {
        if (isArmed == true)
        {
            this.transform.GetChild(0).GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            isArmed = false;
            wasFired = true;
        }
    }


    public void Arm()
    {
        if (isArmed == false)
        {
            isArmed = true;
            wasFired = false;
        }
    }
}
