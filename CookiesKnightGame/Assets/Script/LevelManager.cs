using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;
    [HideInInspector]
    public float timer;
    [HideInInspector]
    public bool updateMaxSlime;
    public GameObject startButton;
    public Sprite[] cookieSprite;
    public Image[] cookieNeededUI;
    [HideInInspector]
    public int[] cookieNeededNow;
    [HideInInspector]
    public bool nextCookiesGenerate = true;

    private int stage = 0, nextStage = 4;
    // Start is called before the first frame update
    void Start()
    {
        if (levelManager == null)
            levelManager = this;
        Time.timeScale = 0f;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (updateMaxSlime)
        {
            EnemyManager.enemyManager.maxSlime++;
            updateMaxSlime = false;
        }
        if (nextCookiesGenerate)
        {
            nextCookiesGenerate = false;
            stage++;
            if(stage > nextStage)
            {
                updateMaxSlime = true;
                nextStage += stage;
            }
            generateNextCookie();
        }
    }

    public void startGame()
    {
        Time.timeScale = 1f;
        Destroy(startButton);
    }

    void generateNextCookie()
    {
        int[] cookiesNeededTemp = { -1, -1, -1 };
        for(int i = 0; i < 3; i++)
        {
            int randomResult = UnityEngine.Random.Range(0, 11);
            while (Array.Exists(cookiesNeededTemp, element => element == randomResult))
            {
                randomResult = UnityEngine.Random.Range(0, 11);
            }
            cookiesNeededTemp[i] = randomResult;
            cookieNeededUI[i].sprite = cookieSprite[randomResult];
            Debug.Log(cookieNeededUI[i].sprite);
        }
        cookieNeededNow = cookiesNeededTemp;
    }

    public void cookiesGet(string cookieType)
    {
        for(int i = 0;  i < 3; i++)
        {
            if(cookieNeededUI[i].sprite.name == cookieType)
            {
                cookieNeededUI[i].color = Color.green;
            }
        }
    }
}
