using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float jumpForce = 0.7f;
    [SerializeField] public int defence = 5;
    [SerializeField] public int power = 10;
    [SerializeField] public float speed = 10;
    [SerializeField] public int health = 100;
    [SerializeField] public int level = 1;
    private bool grounded;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;



    public void SavePlayerState()
    { SavingSystem.SavePlayerState(this); }


    public void LoadPlayerState()
    {
        PlayerData data = SavingSystem.LoadPlayerState();

        level = data.level;
        health = data.health;
        speed = data.speed;
        power = data.power;
        defence = data.defence;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Vector3 normal = collision.GetContact(0).normal;

            if (normal == Vector3.up)
            { grounded = true; }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        { grounded = false; }
    }


    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position += speed * dir * Time.deltaTime;
        sprite.flipX = dir.x < 0.0f;

    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
            Jump();
        if (Input.GetButton("Horizontal"))
            Run();
        
    }

    private void Jump()
    {
        if (grounded)
        { rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse); }
        
    }
}
