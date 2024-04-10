using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    //[SerializeField] private int health = 100;
    [SerializeField] private float jumpForce = 0.7f;
    private bool grounded;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;



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
