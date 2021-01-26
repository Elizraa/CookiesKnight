using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookieControl : MonoBehaviour
{
    private float amplitude = 0.2f;
    public float frequency = 1f;

    public AudioClip pickUp;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {   
            if(Array.Exists(LevelManager.levelManager.cookieNeededNow, element => element == int.Parse(this.gameObject.name)))
            {
                collision.gameObject.GetComponent<AudioSource>().PlayOneShot(pickUp);
                LevelManager.levelManager.cookiesGet(this.gameObject.name);
            }
        }
    }
}
