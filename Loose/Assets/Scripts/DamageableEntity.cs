using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEntity : MonoBehaviour {

    public float health;
    public bool friendlyToPlayer;

    public void takeDamage( float damageAmount)
    {
        GameObject.Find("LevelManager").GetComponent<LvlManager>().IncreaseScore();
        health -= damageAmount;
        if( health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
