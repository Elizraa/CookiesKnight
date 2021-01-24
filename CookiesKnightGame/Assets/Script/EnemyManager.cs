using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager enemyManager;

    public Transform[] spawnPoints;
    public int maxSlime, numberOfSlime;
    public float minSpawnTime, maxSpawnTime;
    public GameObject slime;

    private void Awake()
    {
        if (enemyManager == null)
            enemyManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(numberOfSlime < 2)
        {
            StartCoroutine(spawnSlime());
        }
    }

    IEnumerator spawnSlime()
    {
        numberOfSlime++;
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        Instantiate(slime, spawnPoints[Random.Range(0, 6)].position, Quaternion.identity);
    }
}
