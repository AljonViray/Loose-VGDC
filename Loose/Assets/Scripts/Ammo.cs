using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    public GameObject castleArea;
    public float damage;
    public float waitTime;
    public bool friendlyToPlayer;


    private void Start()
    {
        castleArea = GameObject.Find("CastleArea");
    }

    private void OnTriggerExit(Collider castleArea)
    {
        Destroy(gameObject, waitTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if opposite faction, deal damage
        //Debug.Log("HIT : " + collision.gameObject);
        DamageableEntity colEntity = collision.gameObject.GetComponent<DamageableEntity>();
        if (colEntity != null && colEntity.friendlyToPlayer != friendlyToPlayer)
        {
            collision.gameObject.GetComponent<DamageableEntity>().takeDamage(damage);
        }
    }


}
