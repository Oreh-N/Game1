using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    private enum State
    { Roaming, Dying }

    private State state;

    private EnemyPathfinding enemyPathfinding;
    [SerializeField] private int health = 100;
    public PlayerController player;
    private Animator anim;
    private bool goingRight;
    private KnockBack knockBack;


    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        anim = GetComponent<Animator>();
        enemyPathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }

    private void Start()
    { StartCoroutine(RoamingRoutine()); }

    private void Update()
    {
        if (health <= 0)
        {
            state = State.Dying;
            anim.SetBool("isDying", true);
        }
    }

    public void DestroyEnemy()
    { 
        player.UpdateLevel();
        Destroy(gameObject);
    }

    public void GetHurt(int damage)
    {
        if (state == State.Dying)
            return;

        anim.SetTrigger("getHurt");
        health -= damage;
        knockBack.GetKnockedBack(PlayerController.Instance.transform, 1000f);
    }

    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            float roamPosition = GetRoamingPosition();
            

            if (roamPosition == 0)
                anim.SetBool("isRunning", false);
            else
            {
                if (roamPosition > 0 && goingRight)
                    Flip();
                else if (roamPosition < 0 && !goingRight)
                    Flip();

                anim.SetBool("isRunning", true);
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
