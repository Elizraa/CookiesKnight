using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieManager : MonoBehaviour
{
    public static CookieManager cookieManager;

    public Transform[] spawnPositions;
    public GameObject[] cookies;
    public GameObject potion;
    private int i = 10;
    [HideInInspector]
    public float waitForSpawnPotion = 2f;
    [HideInInspector]
    public bool potionIsIn = true;

    private void Awake()
    {
        if (cookieManager == null)
            cookieManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnCookies();
    }

    // Update is called once per frame
    void Update()
    {
        if (!potionIsIn)
        {
            potionIsIn = true;
            Invoke("spawnPotion", waitForSpawnPotion);
        }
        
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
            GameObject go = Instantiate(cookies[t], spawnPositions[t].position, Quaternion.identity);
            go.transform.name = go.name.Replace("(Clone)", "");
        }
    }

    public void spawnPotion()
    {
        Vector2 position = new Vector2(Random.Range(12.5f, -12.5f), Random.Range(-16.5f, 9.7f));
        LayerMask wall = LayerMask.GetMask("Wall");
        LayerMask house = LayerMask.GetMask("boxNotForSpawn");
        if (i == -1)
        {
            potionIsIn = false;
            waitForSpawnPotion = 2f;
            i = 10;
            return;
        }
        if (Physics2D.OverlapCircle(position, 1f, wall) || Physics2D.OverlapCircle(position, 1f, house))
        {
            if (i < 0) return;
            i--;
            spawnPotion();
        }
        else
        {
            GameObject go = Instantiate(potion, position, Quaternion.identity);
            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, -0.1f);

        }
    }
}
