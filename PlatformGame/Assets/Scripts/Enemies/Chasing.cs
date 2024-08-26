using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Chasing : MonoBehaviour
{
    private EnemyPathfinding enemyPathFinding;
    public Transform playerTransform;
    private EnemyController enemy;
    private EnemyController.State prevState;
    public float chaseDist {  get; private set; }
    


    private void Start()
    {
        enemyPathFinding = GetComponent<EnemyPathfinding>();
        enemy = GetComponent<EnemyController>();
        prevState = enemy.state;
        chaseDist = 5f;
    }

    void Update()
    {
        if (enemy.state == EnemyController.State.Chasing)
        { 
            Chase();
            if (Vector2.Distance(transform.position, playerTransform.position) >= chaseDist)
            { enemy.state = prevState; }
        }
        else if (Vector2.Distance(transform.position, playerTransform.position) < chaseDist)
        {
            prevState = enemy.state;
            enemy.state = EnemyController.State.Chasing;
        }
    }

    private void Chase()
    {
        if (transform.position.x > playerTransform.position.x)  // player on the left
        { enemyPathFinding.MoveTo(Vector3.left.x); }

        if (transform.position.x < playerTransform.position.x)
        { enemyPathFinding.MoveTo(Vector3.right.x); }
    }
}
