using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyController : MonoBehaviour
{
    public enum State
    { Roaming, Dying, Patroling, Chasing }

    public State state;

    [SerializeField] FloatingHealthBar healthBar;
    public bool goingRight;
    public int maxHealth = 100, health = 100;
    public float knockBackForce = 1500f;
    public PlayerController player;

    private KnockBack knockBack;
    private LootSpawn lootSpawn;
    public Animator anim;
    bool isDead = false;


    void Start()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        knockBack = GetComponent<KnockBack>();
        lootSpawn = GetComponent<LootSpawn>();
        anim = GetComponent<Animator>();
        goingRight = true;

        healthBar.InitializeHealthBar(maxHealth, health);
    }

    void Update()
    {
        if (health <= 0)
        { Die(); }
    }

    private void Die()
    {
        if (!isDead)
            lootSpawn.SpawnLoot();

        state = State.Dying;
        anim.SetBool("isDying", true);
        isDead = true;
        DestroyEnemy();
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
        knockBack.GetKnockedBack(PlayerController.Instance.transform, knockBackForce);
        healthBar.UpdateHealthBar(health);
    }

    public void Flip()
    {
        goingRight = !goingRight;
        Vector3 scaler = transform.localScale; // works correctly for enemies of any scale
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public Animator GetAnimator() { return anim; } 
}
