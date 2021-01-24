using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SlimeControl : MonoBehaviour
{
    public AIPath aiPath;
    public GameObject parent;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
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
        if (collision.CompareTag("HouseCookie"))
        {
            anim.SetTrigger("Eat");
            Invoke("Die",1.5f);
        }

        if (collision.CompareTag("AttackPoint"))
        {
            anim.SetTrigger("Die");
            Invoke("Die",0.7f);
        }
    }

    void Die()
    {
        Destroy(parent);
        EnemyManager.enemyManager.numberOfSlime--;
    }
}
