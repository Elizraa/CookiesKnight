using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicAndMenu : MonoBehaviour
{
    public static MusicAndMenu musicAndMenu;
    public GameObject screenFader;
    public Animator anim;

    void Awake()
    {
        if (musicAndMenu == null)
        {
            DontDestroyOnLoad(this.gameObject);
            DontDestroyOnLoad(screenFader);
            musicAndMenu = this;
        }
        else if (musicAndMenu != this)
            Destroy(gameObject);
    }

    public void LoadScene()
    {
        if (SceneManager.GetActiveScene().name == "StartMenu")
        {
            PlayFader();
            SceneManager.LoadScene(1);
        }
    }
    public void Menu()
    {
        PlayFader();
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        PlayFader();
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    void PlayFader()
    {
        Time.timeScale = 1f;
        anim.Play("FadeInFader");
    }
}
