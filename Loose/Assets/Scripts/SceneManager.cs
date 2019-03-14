using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    private Text gameOverText;
    private LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        gameOverText = gameOverPanel.transform.GetChild(0).GetComponent<Text>();
        gameOverPanel.SetActive(false);
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseGame()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        gameOverText.text = "You Lose\nScore: " + levelManager.score.ToString();
    }
}
