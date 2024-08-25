using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDist;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D enemyCollider;
    [SerializeField] private LayerMask playerLayer;
    private float coolDownTimer = Mathf.Infinity;
    private Animator anim;

    public PlayerController player;
    private EnemyController enemy;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemy = GetComponent<EnemyController>();
    }

    private void Update()
    {
        coolDownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (coolDownTimer >= attackCoolDown)
            {
                coolDownTimer = 0;
                anim.SetTrigger("meleeAttack");
            }
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(enemyCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDist, 
            new Vector3(enemyCollider.bounds.size.x * range, enemyCollider.bounds.size.y, enemyCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        { player = hit.transform.GetComponent<PlayerController>(); }

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(enemyCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDist,
            new Vector3(enemyCollider.bounds.size.x * range, enemyCollider.bounds.size.y, enemyCollider.bounds.size.z));
    }

    private void HitPlayer()
    {
        if (PlayerInSight()) // if player still in hitbox
        {
            enemy.state = EnemyController.State.Attacking;
            player.TakeDamage(damage);
        }
        else
            enemy.state = EnemyController.State.Roaming;
    }
}
