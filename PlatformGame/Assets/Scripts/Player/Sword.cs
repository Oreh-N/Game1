using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private float timeBtwAttack;
    public float startTimeBtwAttack = 0.3f;


    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public int damage = 20;
    public Animator attackAnim;


    private void Update()
    {
        if (timeBtwAttack <= 0)
        {
            if (Input.GetKey(KeyCode.Alpha0))
            {
                attackAnim.SetTrigger("Attack");

                Vector3 attackDirection = transform.localScale.x > 0 ? Vector3.right : Vector3.left;
                attackPos.localPosition = new Vector3(Mathf.Abs(attackPos.localPosition.x) * Mathf.Sign(transform.localScale.x), attackPos.localPosition.y, attackPos.localPosition.z);

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyController>().GetHurt(damage);
                }
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
