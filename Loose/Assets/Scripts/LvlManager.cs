using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlManager : MonoBehaviour
{
    public int score;
    public int enemiesInPlay;

    private int enemiesToSpawn;
    private int enemiesSpawned;
    private float spawnTimer;
    private float timeSinceSpawn;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        enemiesToSpawn = 5;
        enemiesSpawned = 0;
        spawnTimer = 10.0f;
        timeSinceSpawn = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= spawnTimer)
        {
            if (enemiesSpawned < enemiesToSpawn)
                SpawnEnemy();
        }
        else if (enemiesSpawned == enemiesToSpawn && enemiesInPlay == 0)
        {
            ResetRond();
        }
    }

    public void IncreaseScore()
    {
        ++score;
        --enemiesInPlay;
    }

    private void SpawnEnemy()
    {
        GameObject.Find("EnemyController").GetComponent<EnemyController>().spawnSiegeTower();
        ++enemiesSpawned;
        ++enemiesInPlay;
        timeSinceSpawn = 0.0f;
    }

    private void ResetRond()
    {
        enemiesSpawned = 0;
        enemiesToSpawn += 5;
        timeSinceSpawn = 0.0f;
    }
}
