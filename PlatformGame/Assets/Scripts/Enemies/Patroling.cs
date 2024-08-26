using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroling : MonoBehaviour
{
    private EnemyPathfinding enemyMovement;
    private EnemyController enemy;
    public Transform[] patrolPoints;
    private int patrolDestination;


    void Start()
    { 
        enemyMovement = GetComponent<EnemyPathfinding>();
        enemy = GetComponent<EnemyController>();
    }

    void Update()
    {
        if (enemy.state != EnemyController.State.Patroling) return;

        enemy.GetAnimator().SetBool("isRunning", true);
        // current enemy position
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[patrolDestination].position, enemyMovement.speed * Time.deltaTime);

        // arrived at the current patrol point
        if (Vector2.Distance(transform.position, patrolPoints[patrolDestination].position) < .2f)
        {
            patrolDestination = (patrolDestination + 1) % patrolPoints.Length;  // selects the next patrol point

            if (patrolDestination % 2 == 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }

        if ((enemy.goingRight && enemyMovement.moveDir < 0) || (!enemy.goingRight && enemyMovement.moveDir > 0))
        { enemy.Flip(); } // the enemy is looking in the opposite direction from the direction of his movement
    }
}
