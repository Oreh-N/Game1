using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wandering : MonoBehaviour
{
    private EnemyPathfinding enemyPathfinding;
    private EnemyController enemy;
    private bool goingRight;
    public float moveDir;
    

    private void Start()
    { StartCoroutine(RoamingRoutine()); }

    private void Awake()
    {
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        enemy = GetComponent<EnemyController>();
    }

    private IEnumerator RoamingRoutine()
    {
        while (enemy.GetState() == EnemyController.State.Roaming)
        {
            float roamPosition = GetRoamingPosition();


            if (roamPosition == 0)
                enemy.GetAnimator().SetBool("isRunning", false);
            else
            {
                if (roamPosition > 0 && goingRight)
                    Flip();
                else if (roamPosition < 0 && !goingRight)
                    Flip();

                enemy.GetAnimator().SetBool("isRunning", true);
            }

            enemyPathfinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(2f);
        }
    }

    void Flip()
    {
        goingRight = !goingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private float GetRoamingPosition()
    { return Random.Range(-1f, 1f); }
}
