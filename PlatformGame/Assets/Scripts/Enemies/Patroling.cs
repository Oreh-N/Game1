using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroling : MonoBehaviour
{
    private EnemyController enemy;
    public Transform[] patrolPoints;
    public int patrolDestination;
    public float moveSpeed;


    private void Start()
    { enemy = GetComponent<EnemyController>(); }

    void Update()
    {
        enemy.GetAnimator().SetBool("isRunning", true);
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[patrolDestination].position, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolPoints[patrolDestination].position) < .2f)
        {
            patrolDestination = (patrolDestination + 1) % patrolPoints.Length;

            if (patrolDestination % 2 == 0)
                transform.localScale = new Vector3(1, 1, 1);
            else
                transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
