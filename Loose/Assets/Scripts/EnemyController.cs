using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject siegeTowerPrefab;
    float spawnEuler;

    void Start ()
    {
        //spawnSiegeTower();
	}
	

    public void spawnSiegeTower()
    {
        float randomDir = Random.Range(0, 359);
        Vector3 spawnLocation = new Vector3(Mathf.Cos(randomDir), 0, Mathf.Sin(randomDir)) * 400;
        int spawnCounter = 0;
        while (CheckForSpawn(spawnLocation) && spawnCounter < 400)
        {
            randomDir = Random.Range(0, 359);
            spawnLocation = new Vector3(Mathf.Cos(randomDir), 0, Mathf.Sin(randomDir)) * 400;
            ++spawnCounter;
        }
        GameObject newSiegeTower = GameObject.Instantiate(siegeTowerPrefab, spawnLocation, Quaternion.identity);
        newSiegeTower.transform.rotation = Quaternion.Slerp(newSiegeTower.transform.rotation
           , Quaternion.LookRotation(new Vector3(0, 0, 0) - newSiegeTower.transform.position), 1f);
        newSiegeTower.transform.Translate(0, 0, 0);
    }


    private bool CheckForSpawn(Vector3 location)
    {
        Vector3 tempSpawn = new Vector3(location.x, location.y + 31.0f, location.z);
        return Physics.CheckSphere(tempSpawn, 30.0f);
    }
}
