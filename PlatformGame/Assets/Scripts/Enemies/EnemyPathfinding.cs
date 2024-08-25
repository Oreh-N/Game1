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
    private EnemyController enemy;


    private void Awake()
    {
        enemy = GetComponent<EnemyController>();
        knockBack = GetComponent<KnockBack>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (enemy.state == EnemyController.State.Attacking)
        {
            enemy.anim.SetBool("isRunning", false);
            enemy.anim.SetTrigger("meleeAttack");
            return;
        }
        if (knockBack.gettingKnockedBack) { return; }
        rb.MovePosition(new Vector2(rb.position.x + moveDir * (speed * Time.fixedDeltaTime), rb.position.y));
    }

    public void MoveTo(float targetPos)
    { moveDir = targetPos; }
}
