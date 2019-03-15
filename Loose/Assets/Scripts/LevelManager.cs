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
        scoreText = canvasScoreTextObj.GetComponent<Text>();
        ResetValues();
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

    public void ResetValues()
    {
        score = 0;
        enemiesToSpawn = 5;
        enemiesSpawned = 0;
        spawnTimer = 20.0f;
        timeSinceSpawn = 0.0f;
        scoreText.text = "Score: 0";
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
