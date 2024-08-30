using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wandering : MonoBehaviour
{
    private EnemyPathfinding enemyMovement;
    private Coroutine roamingCoroutine;
    private EnemyController enemy;
    

    private void Start()
    { 
        enemyMovement = GetComponent<EnemyPathfinding>();
        enemy = GetComponent<EnemyController>();
        StartRoaming();
    }

    private void Update()
    {                                                   // got distracted by the player and stopped roaming
        if (enemy.state == EnemyController.State.Roaming && roamingCoroutine == null)
        { StartRoaming(); }

        if ((enemy.goingRight && enemyMovement.moveDir < 0) || (!enemy.goingRight && enemyMovement.moveDir > 0))
        { enemy.Flip(); } // the enemy is looking in the opposite direction from the direction of his movement
    }

    private void StartRoaming()
    { roamingCoroutine = StartCoroutine(RoamingRoutine()); }

    private IEnumerator RoamingRoutine()
    {
        while (enemy.state == EnemyController.State.Roaming)
        {
            float roamPosition = GetRoamingPosition();


            if (roamPosition >= -0.1f && roamPosition <= 0.1f)
            { enemy.GetAnimator().SetBool("isRunning", false); }
            else
            {
                enemy.GetAnimator().SetBool("isRunning", true);
                enemyMovement.MoveTo(roamPosition);
            }
            yield return new WaitForSeconds(2f);
        }
        roamingCoroutine = null;    // stop roaming
    }

    private float GetRoamingPosition()
    { return Random.Range(-1f, 1f); }

    private void OnCollisionEnter2D(Collision2D collision)
    {   // the direction of the enemy's movement is towards the wall
        if (collision.gameObject.CompareTag("Wall"))
        { enemyMovement.MoveTo(-enemyMovement.moveDir); }
    }
}
