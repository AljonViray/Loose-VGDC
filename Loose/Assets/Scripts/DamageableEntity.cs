using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEntity : MonoBehaviour {

    public float health;
    public bool friendlyToPlayer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void takeDamage( float damageAmount)
    {
        health -= damageAmount;
        if( health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
