using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float pauseBtwAttack = 0.2f;

    public LayerMask whatIsEnemy;
    public Transform attackPos;
    public float attackRange;
    public int damage = 20;
    private Animator anim;

    bool isAttacking = false;
    

    private void Start()
    { anim = GetComponent<Animator>(); }

    private void Update()
    {
        if (isAttacking) return;

        if (Input.GetKeyDown(KeyCode.Return))
            StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        isAttacking = true;

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);

        for (int i = 0; i < enemiesToDamage.Length; i++)
        { enemiesToDamage[i].GetComponent<EnemyController>().GetHurt(damage); }

        anim.SetTrigger("hit");
        
        yield return new WaitForSeconds(pauseBtwAttack);
        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
