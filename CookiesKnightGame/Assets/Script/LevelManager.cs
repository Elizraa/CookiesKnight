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
    public List<Image> cookieNeededUI = new List<Image>();
    [HideInInspector]
    public int[] cookieNeededNow;
    [HideInInspector]
    public bool nextCookiesGenerate = true;

    public Slider slider;
    public Gradient gradientTimer;
    public Image timerImage;

    private float timeOfTheStage = 210f;

    [HideInInspector]
    public int stage = 0, nextStage = 4;
    private float timeLog = 0f;

    public AudioSource mainMusic;
    public AudioSource playerAudio;
    public AudioClip doneStageSound,pickUpCookie;

    public ParticleSystem playerParticle;

    // Start is called before the first frame update
    void Start()
    {
        if (levelManager == null)
            levelManager = this;
        Time.timeScale = 0f;
        timer = 0f;
        setMaxTime(timeOfTheStage);
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
            if (stage > nextStage)
            {
                updateMaxSlime = true;
                nextStage += stage;
            }
            if (timeOfTheStage > 130f)
                timeOfTheStage -= 10f;
            timeLog = timer;
            setMaxTime(timeOfTheStage);
            generateNextCookie();
        }
        updateTimer();
    }

    public void startGame()
    {
        Time.timeScale = 1f;
        mainMusic.enabled = false;
        Destroy(startButton);
    }

    void generateNextCookie()
    {
        int[] cookiesNeededTemp = { -1, -1, -1 };
        for(int i = 0; i < 3; i++)
        {
            int randomResult;
            do
            {
                randomResult = UnityEngine.Random.Range(0, 11);
            }while (Array.Exists(cookiesNeededTemp, element => element == randomResult));
            cookiesNeededTemp[i] = randomResult;
            cookieNeededUI.Add(GameObject.Find("CookieImage" + i.ToString()).GetComponent<Image>());
            cookieNeededUI[i].sprite = cookieSprite[randomResult];
        }
        for(int i = 0; i < cookieNeededUI.Count; i++)
        {
            cookieNeededUI[i].color = Color.white;
        }
        cookieNeededNow = cookiesNeededTemp;
    }

    public void cookiesGet(string cookieType)
    {
        for(int i = 0;  i < cookieNeededUI.Count; i++)
        {
            if(cookieNeededUI[i].sprite.name == cookieType)
            {
                cookieNeededUI[i].color = Color.green;
                playerAudio.PlayOneShot(pickUpCookie);
                playerParticle.Play();
                cookieNeededUI.RemoveAt(i);
            }
        }
        if(cookieNeededUI.Count == 1)
        {
            stage++;
            nextCookiesGenerate = true;
            playerAudio.PlayOneShot(doneStageSound);
            HouseHealth.houseHealth.updateScore(150);
        }
    }

    public void setMaxTime(float timeThisStage)
    {
        slider.maxValue = timeThisStage;
        slider.value = timeThisStage;
        timerImage.color = gradientTimer.Evaluate(1f);
    }

    void updateTimer()
    {
        slider.value = timeOfTheStage - (timer - timeLog);
        timerImage.color = gradientTimer.Evaluate(slider.normalizedValue);
        if (slider.value <= 0)
        {
            HouseHealth.houseHealth.healthReduce();
            nextCookiesGenerate = true;
        }
    }
}
