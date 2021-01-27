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

    public void Menu()
    {
        PlayFader();
        StartCoroutine(changeSceneTO(0));
    }

    public void Retry()
    {
        PlayFader();
        StartCoroutine(changeSceneTO(1));
    }

    void PlayFader()
    {
        Time.timeScale = 1f;
        anim.Play("FadeInFader");
    }

    IEnumerator changeSceneTO(int sceneIndex)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneIndex);
    }
}
