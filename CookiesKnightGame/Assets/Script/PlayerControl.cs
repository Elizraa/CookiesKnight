using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    public Transform playerGraf;
    public Animator anim;
    public GameObject minimap;

    private AudioSource audioSource;
    public AudioClip attack;
    Vector2 movement;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Fire1")) Attack();
        if (Input.GetButtonDown("Fire2")) showMinimap();
        else if (Input.GetButtonUp("Fire2")) closeMinimap();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if (movement != Vector2.zero) anim.SetBool("Run", true);
        else anim.SetBool("Run", false);

        if (movement.y > 0.01f) playerGraf.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        else if (movement.y < -0.01f) playerGraf.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
        else if (movement.x > 0.01f) playerGraf.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
        else if (movement.x < -0.01f) playerGraf.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
    }

    void Attack()
    {
        audioSource.PlayOneShot(attack);
        anim.SetTrigger("Attack");
    }

    void showMinimap()
    {
        minimap.SetActive(true);
    }

    void closeMinimap()
    {
        minimap.SetActive(false);
    }
}
