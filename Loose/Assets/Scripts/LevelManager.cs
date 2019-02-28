using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int score;
    public int enemiesInPlay;
    public GameObject canvasScoreTextObj;

    private int enemiesToSpawn;
    private int enemiesSpawned;
    private float spawnTimer;
    private float timeSinceSpawn;
    private Text scoreText;


    void Start()
    {
        score = 0;
        enemiesToSpawn = 5;
        enemiesSpawned = 0;
        spawnTimer = 10.0f;
        timeSinceSpawn = 0.0f;
        scoreText = canvasScoreTextObj.GetComponent<Text>();
        scoreText.text = "Score: 0";
    }


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
            ResetRound();
        }
    }


    public void IncreaseScore()
    {
        ++score;
        --enemiesInPlay;
        scoreText.text = "Score: " + score.ToString();
    }


    private void SpawnEnemy()
    {
        GameObject.Find("EnemyController").GetComponent<EnemyController>().spawnSiegeTower();
        ++enemiesSpawned;
        ++enemiesInPlay;
        timeSinceSpawn = 0.0f;
    }


    private void ResetRound()
    {
        enemiesSpawned = 0;
        enemiesToSpawn += 5;
        timeSinceSpawn = 0.0f;
    }
}
