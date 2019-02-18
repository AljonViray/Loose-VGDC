using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    public GameObject castleArea;
    public GameObject catapult;
    public TrailRenderer trail;
    public float damage;
    public bool friendlyToPlayer;
    public bool outsideOfCastle;


    private void Start()
    {
        castleArea = GameObject.Find("CastleArea");
        catapult = GameObject.Find("Catapult");
        trail = this.gameObject.GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (catapult.GetComponent<Catapult>().isArmed && outsideOfCastle == true)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider castleArea)
    {
        trail.enabled = true;
        outsideOfCastle = true;
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
