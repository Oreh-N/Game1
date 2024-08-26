using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public enum State
    { Roaming, Dying, Patroling }

    private State state;

    [SerializeField] private int health = 100;
    public PlayerController player;
    private KnockBack knockBack;
    private Animator anim;
    
    
    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        anim = GetComponent<Animator>();
        state = State.Roaming;
    }

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

    public State GetState() { return state; }

    public Animator GetAnimator() { return anim; } 
}
