using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Color color1;
    private int health = 2;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = gameObject.GetComponent<SpriteRenderer>();
        color1 = new Color(1f, 1f, 1f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackPoint"))
        {
            health--;
            if (health == 0)
            {
                Destroy(gameObject);
                return;
            }
            sprite.color = color1;
        }
    }
}
