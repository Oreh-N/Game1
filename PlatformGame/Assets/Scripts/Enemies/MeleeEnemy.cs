using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private BoxCollider2D enemyCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float colliderDist;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    private float coolDownTimer = Mathf.Infinity;
    public PlayerController player;
    private Animator anim;


    private void Awake()
    { anim = GetComponent<Animator>(); }

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
            new Vector3(enemyCollider.bounds.size.x * range, enemyCollider.bounds.size.y, enemyCollider.bounds.size.z), 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null) // the player is inside the hitbox
        { player = hit.transform.GetComponent<PlayerController>(); }

        return hit.collider != null;
    }

    private void OnDrawGizmos()     // hitbox visualization
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(enemyCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDist,
            new Vector3(enemyCollider.bounds.size.x * range, enemyCollider.bounds.size.y, enemyCollider.bounds.size.z));
    }

    private void HitPlayer()    // used as animation event
    {
        if (PlayerInSight()) // if player still in hitbox
        { player.health.TakeDamage(damage); }
    }
}
