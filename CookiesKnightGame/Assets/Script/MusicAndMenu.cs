using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicAndMenu : MonoBehaviour
{
    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

    public void LoadScene()
    {
        if (SceneManager.GetActiveScene().name == "StartMenu")
        {
            SceneManager.LoadScene(1);
        }
    }
}
