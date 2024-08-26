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

    [SerializeField] private int health = 100;
    public PlayerController player;
    private KnockBack knockBack;
    [SerializeField] public bool goingRight {  get; private set; }
    private Animator anim;
    
    
    private void Start()
    {
        knockBack = GetComponent<KnockBack>();
        anim = GetComponent<Animator>();
        state = State.Roaming;
        goingRight = true;
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

    public void Flip()
    {
        goingRight = !goingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    public Animator GetAnimator() { return anim; } 
}
