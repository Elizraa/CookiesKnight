using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager enemyManager;

    public Transform[] spawnPoints, spawnPointsDoor;
    public int maxSlime, numberOfSlime;
    public float minSpawnTime, maxSpawnTime;
    public GameObject slime,door;

    private int randomResult = -1;

    private void Awake()
    {
        if (enemyManager == null)
            enemyManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(numberOfSlime < maxSlime)
        {
            StartCoroutine(spawnSlime());
        }
    }

    IEnumerator spawnSlime()
    {
        numberOfSlime++;
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        int currentRandom;
        do
        {
            currentRandom = Random.Range(0, 6);
        } while (currentRandom == randomResult);
        randomResult = currentRandom;
        Instantiate(slime, spawnPoints[randomResult].position, Quaternion.identity);
    }

    public void spawnDoor()
    {
        Instantiate(door, spawnPointsDoor[Random.Range(0, 4)].position, Quaternion.identity);
    }
}
