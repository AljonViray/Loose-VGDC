using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    public float damage;
    public bool friendlyToPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        //if opposite faction, deal damage
        Debug.Log("HIT : " + collision.gameObject);
        DamageableEntity colEntity = collision.gameObject.GetComponent<DamageableEntity>();
        if (colEntity != null && colEntity.friendlyToPlayer != friendlyToPlayer)
        {
            collision.gameObject.GetComponent<DamageableEntity>().takeDamage(damage);
        }
    }


}
