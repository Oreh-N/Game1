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

    public bool goingRight { get; private set; }
    public PlayerController player;
    private KnockBack knockBack;
    private int health = 100;
    private Animator anim;
    
    
    void Start()
    {
        knockBack = GetComponent<KnockBack>();
        anim = GetComponent<Animator>();
        state = State.Roaming;
        goingRight = true;
    }

    void Update()
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

    public void Flip()
    {
        goingRight = !goingRight;
        Vector3 scaler = transform.localScale; // works correctly for enemies of any scale
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public Animator GetAnimator() { return anim; } 
}
