using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private float moveDir;
    private KnockBack knockBack;


    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (knockBack.gettingKnockedBack) { return; }
        rb.MovePosition(new Vector2(rb.position.x + moveDir * (speed * Time.fixedDeltaTime), rb.position.y));
    }

    public void MoveTo(float targetPos)
    { moveDir = targetPos; }
}
