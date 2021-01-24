using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieManager : MonoBehaviour
{
    public Transform[] spawnPositions;
    public GameObject[] cookies;

    // Start is called before the first frame update
    void Start()
    {
        spawnCookies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnCookies()
    {
        for (int t = 0; t < spawnPositions.Length; t++)
        {
            Transform tmp = spawnPositions[t];
            int r = Random.Range(t, spawnPositions.Length);
            spawnPositions[t] = spawnPositions[r];
            spawnPositions[r] = tmp;
        }
        for(int t = 0; t < cookies.Length; t++)
        {
            Instantiate(cookies[t], spawnPositions[t].position, Quaternion.identity);
        }
    }
}
