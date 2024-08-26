using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] public float speed { get; private set; }
    private KnockBack knockBack;
    private Rigidbody2D rb;
    public float moveDir {  get; private set; }
    

    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        rb = GetComponent<Rigidbody2D>();
        speed = 5f;
    }

    private void Update()
    {
        if (knockBack.gettingKnockedBack) { return; }
        // goes in the current direction
        rb.MovePosition(new Vector2(rb.position.x + moveDir * (speed * Time.fixedDeltaTime), rb.position.y));
    }

    public void MoveTo(float targetPos)
    { moveDir = targetPos; }    // set current direction
}
