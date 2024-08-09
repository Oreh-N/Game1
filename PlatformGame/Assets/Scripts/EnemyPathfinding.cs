using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float speed = 2f;

    private Rigidbody2D rb;
    private float moveDir;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(new Vector2(rb.position.x + moveDir * (speed * Time.fixedDeltaTime), rb.position.y));
    }

    public void MoveTo(float targetPos)
    {
        moveDir = targetPos;
    }
}
