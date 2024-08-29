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
        if ((enemy.goingRight && enemyMovement.moveDir < 0) || (!enemy.goingRight && enemyMovement.moveDir > 0))
        { enemy.Flip(); } // the enemy is looking in the opposite direction from the direction of his movement

        if (enemy.state != EnemyController.State.Patroling) return;

        enemy.GetAnimator().SetBool("isRunning", true);

        // arrived at the current patrol point
        if (Vector2.Distance(transform.position, patrolPoints[patrolDestination].position) < .2f)
            patrolDestination = (patrolDestination + 1) % patrolPoints.Length;  // selects the next patrol point

        CheckDirection();
    }

    private void CheckDirection()
    {
        if (transform.position.x - patrolPoints[patrolDestination].position.x > 0)  // go left
        {
            transform.localScale = new Vector3(-1, 1, 1);
            enemy.goingRight = false;
            enemyMovement.MoveTo(-1);
        }
        else if (transform.position.x - patrolPoints[patrolDestination].position.x < 0)  // go right
        {
            transform.localScale = new Vector3(1, 1, 1);
            enemy.goingRight = true;
            enemyMovement.MoveTo(1);
        }
    }
}
