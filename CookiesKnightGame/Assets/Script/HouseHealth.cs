using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HouseHealth : MonoBehaviour
{
    public static HouseHealth houseHealth;

    [HideInInspector]
    public int health = 5;
    public Image[] healthImage;

    [HideInInspector]
    public int Score = 0;
    public Text scoreText;

    public AudioClip gameOverSound;

    public GameObject gameOverPanel;
    public Text finalScore, finalPackage;
    // Start is called before the first frame update
    void Start()
    {
        if (houseHealth == null)
            houseHealth = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void healthReduce()
    {
        health--;
        healthImage[health].color = Color.blue;
        if(health == 0)
        {
            gameOver();
        }
    }

    public void gameOver()
    {
        Time.timeScale = 0f;
        finalScore.text = Score.ToString();
        finalPackage.text = (LevelManager.levelManager.stage - 1).ToString();
        gameOverPanel.SetActive(true);
        LevelManager.levelManager.mainMusic.enabled = true;
        LevelManager.levelManager.playerAudio.PlayOneShot(gameOverSound);
    }

    public void updateScore(int scoreUpdate)
    {
        Score += scoreUpdate;
        scoreText.text = Score.ToString();
    }

    public void Menu()
    {

    }

    public void Retry()
    {
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }
}
