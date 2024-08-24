using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack = 0.3f;


    private Animator anim;
    public LayerMask whatIsEnemy;
    public Transform attackPos;
    public float attackRange;
    public int damage = 20;



    private void Start()
    { anim = GetComponent<Animator>(); }

    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Alpha0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                anim.SetTrigger("hit");
                for (int i = 0; i < enemiesToDamage.Length; i++)
                    enemiesToDamage[i].GetComponent<EnemyController>().GetHurt(damage);
            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
