using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wandering : MonoBehaviour
{
    private EnemyPathfinding enemyPathfinding;
    private EnemyController enemy;
    private bool goingRight;
    

    private void Start()
    { StartCoroutine(RoamingRoutine()); }

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        enemy = GetComponent<EnemyController>();
    }

    private IEnumerator RoamingRoutine()
    {
        while (enemy.state == EnemyController.State.Roaming)
        {
            float roamPosition = GetRoamingPosition();


            if (roamPosition == 0)
                enemy.GetAnimator().SetBool("isRunning", false);
            else
            {
                if (roamPosition > 0 && goingRight)
                    enemy.Flip(ref goingRight);
                else if (roamPosition < 0 && !goingRight)
                    enemy.Flip(ref goingRight);

                enemy.GetAnimator().SetBool("isRunning", true);
            }

            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    private float GetRoamingPosition()
    { return Random.Range(-1f, 1f); }
}
