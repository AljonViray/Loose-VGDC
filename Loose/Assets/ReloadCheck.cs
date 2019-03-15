using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Arm")
        {
            this.transform.parent.parent.GetComponent<Catapult>().isArmed = true;
            other.transform.localRotation = Quaternion.Euler(110, 0, 0);
        }
    }
}
