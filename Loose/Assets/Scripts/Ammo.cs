using System;
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
    public bool canDamage = true;


    private void Start()
    {
        castleArea = GameObject.Find("CastleArea");
        catapult = GameObject.Find("Catapult");
        trail = this.gameObject.GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (gameObject.transform.position.y < 1 && canDamage && outsideOfCastle)
        {
            canDamage = false;
        }

        if (catapult.GetComponent<Catapult>().isArmed && outsideOfCastle)
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
        //Debug.Log("HIT : " + collision.gameObject);
        DamageableEntity colEntity = collision.gameObject.GetComponent<DamageableEntity>();

        if (colEntity != null && colEntity.friendlyToPlayer != friendlyToPlayer && canDamage)
        {
            collision.gameObject.GetComponent<DamageableEntity>().takeDamage(damage);
        }
    }


}
