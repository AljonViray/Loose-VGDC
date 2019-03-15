using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeTower : MonoBehaviour {


    public float moveSpeed;
    Rigidbody rb;
    private bool canMove;

    private float pauseMove;
	// Use this for initialization
	void Start () {
        rb = this.gameObject.GetComponent<Rigidbody>();
        canMove = true;
        pauseMove = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (canMove)
        {
            rb.MovePosition(this.gameObject.transform.position + this.gameObject.transform.forward * moveSpeed * Time.deltaTime);
        }
        else
        {
            if (pauseMove < 5.0f)
                pauseMove += Time.deltaTime;
            else
                canMove = true;
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Enemy"))
        {
            SiegeTower towerScript = collision.gameObject.GetComponent<SiegeTower>();
            int num = Random.Range(0, 2);
            if (num == 0)
                this.canMove = false;
            else
            {
                if (this.canMove == true)
                    towerScript.canMove = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Castle"))
        {
            SceneManagerScript sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
            sceneManager.LoseGame();
        }
    }
}
