using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    public GameObject siegeTowerPrefab;

    float spawnEuler;
	// Use this for initialization
	void Start () {
        spawnSiegeTower();
	}
	
	// Update is called once per frame
	void Update () {

        

	}

    public void spawnSiegeTower()
    {
        float randomDir = Random.Range(0, 359);
        Vector3 spawnLocation = new Vector3(Mathf.Cos(randomDir), 0, Mathf.Sin(randomDir)) * 400;

        GameObject newSiegeTower = GameObject.Instantiate(siegeTowerPrefab, spawnLocation, Quaternion.identity);
        newSiegeTower.transform.rotation = Quaternion.Slerp(newSiegeTower.transform.rotation
            , Quaternion.LookRotation(new Vector3(0,0, 0) - newSiegeTower.transform.position), 1f);
        newSiegeTower.transform.Translate(0, 0, 0);

    }
}
