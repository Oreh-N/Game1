using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wandering : MonoBehaviour
{
    private EnemyPathfinding enemyPathfinding;
    private Coroutine roamingCoroutine;
    private EnemyController enemy;
    private bool goingRight;
    
    

    private void Start()
    { 
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        enemy = GetComponent<EnemyController>();
        StartRoaming();
    }

    private void Update()
    {
        if (enemy.state == EnemyController.State.Roaming && roamingCoroutine == null)
        { StartRoaming(); }
    }

    private void StartRoaming()
    { roamingCoroutine = StartCoroutine(RoamingRoutine()); }

    private IEnumerator RoamingRoutine()
    {
        while (enemy.state == EnemyController.State.Roaming)
        {
            float roamPosition = GetRoamingPosition();


            if (roamPosition == 0)
            {
                enemy.GetAnimator().SetBool("isRunning", false);
                enemyPathfinding.MoveTo(0);
            }
            else
            {
                if (roamPosition > 0 && !goingRight)
                    enemy.Flip(ref goingRight);
                else if (roamPosition < 0 && goingRight)
                    enemy.Flip(ref goingRight);

                enemy.GetAnimator().SetBool("isRunning", true);
                enemyPathfinding.MoveTo(roamPosition);
            }
            yield return new WaitForSeconds(2f);
        }
        roamingCoroutine = null;
    }

    private float GetRoamingPosition()
    { return Random.Range(-1f, 1f); }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            goingRight = !goingRight;
            enemy.Flip(ref goingRight);

            enemyPathfinding.MoveTo(-enemyPathfinding.moveDir);
        }
    }
}
