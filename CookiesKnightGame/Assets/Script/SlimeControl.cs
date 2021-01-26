using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SlimeControl : MonoBehaviour
{
    public AIPath aiPath;
    public GameObject parent;
    private Animator anim;
    private AudioSource audioSource;
    public AudioClip eatSound, hitSound;
    bool touch;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        rotateSlime();
    }

    void rotateSlime()
    {
        if (aiPath.desiredVelocity.x >= 0.01f) transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (aiPath.desiredVelocity.x <= 0.01f) transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!touch)
        {
            if (collision.CompareTag("HouseCookie"))
            {
                touch = true;
                anim.SetTrigger("Eat");
                {
                    eat = true;
                    audioSource.PlayOneShot(eatSound);
                    HouseHealth.houseHealth.healthReduce();
                    Invoke("Die", 1.5f);
                }
            }

            if (collision.CompareTag("AttackPoint"))
            {
                touch = true;
                anim.SetTrigger("Die");
                audioSource.PlayOneShot(hitSound);
                HouseHealth.houseHealth.updateScore(5);
                Invoke("Die", 0.7f);
            }
        }
    }

    void Die()
    {
            EnemyManager.enemyManager.numberOfSlime--;
        Destroy(parent);
    }
}
