using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    public float moveDir;
    private KnockBack knockBack;

    public Transform playerTransform;
    public bool isChasing;
    public float chaseDist;

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isChasing)
        {
            Chase();
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDist)
                isChasing = true;

            if (knockBack.gettingKnockedBack) { return; }

            rb.MovePosition(new Vector2(rb.position.x + moveDir * (speed * Time.fixedDeltaTime), rb.position.y));
        }
    }

    private void Chase()
    {
        if (transform.position.x > playerTransform.position.x)  // player on the left
        {                               // enemy scale
            if (moveDir > 0)
                transform.localScale = new Vector3(-1, 1, 1);
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (transform.position.x < playerTransform.position.x)
        {
            if (moveDir < 0)
                transform.localScale = new Vector3(1, 1, 1);
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        isChasing = false;
    }

    public void MoveTo(float targetPos)
    { moveDir = targetPos; }
}
