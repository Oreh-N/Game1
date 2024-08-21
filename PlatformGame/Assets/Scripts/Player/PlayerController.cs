using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController  : MonoBehaviour
{
    public GameObject interactIcon;

    [SerializeField] public float jumpForce = 11f;
    [SerializeField] public int defence = 5;
    [SerializeField] public int power = 10;
    [SerializeField] public float speed = 5;
    [SerializeField] public int health = 100;
    [SerializeField] public int level = 1;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D boxCollider2d;
    [SerializeField] private LayerMask platformsLayerMask;
    [SerializeField] private LayerMask enemyLayerMask;
    private Vector2 boxSize = new Vector2 (1.5f, 1f);
    public SpriteRenderer sword;
    private bool goingRight;

    private Animator anim;


    public void SavePlayerState()
    { SavingSystem.SavePlayerState(this); }

    public void LoadPlayerState()
    {
        PlayerData data = SavingSystem.LoadPlayerState();

        level = data.level;
        health = data.health;
        speed = data.speed;
        power = data.power;
        defence = data.defence;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }


    void Update()
    {
        if (IsGrounded())
        { anim.SetBool("isJumping", false); }
        else
        { anim.SetBool("isJumping", true); }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        { 
            Jump();
            anim.SetTrigger("takeOff");
        }

        if (Input.GetButton("Horizontal"))
        {
            Move();
            if (goingRight && Input.GetAxis("Horizontal") > 0)
                Flip();
            else if (goingRight == false && Input.GetAxis("Horizontal") < 0)
                Flip();
        }

        if (Input.GetKeyDown(KeyCode.E))
            CheckInteraction();

        if (Input.GetAxis("Horizontal") == 0)
            anim.SetBool("isRunning", false);
        else
            anim.SetBool("isRunning", true);
    }

    private void Move()
    {
        float dir = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dir * speed, rb.velocity.y);
    }

    private void Jump()
    { rb.velocity = Vector2.up * jumpForce; }

    void Flip()
    {
        goingRight = !goingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2dPlatform = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        RaycastHit2D raycastHit2dEnemy = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, enemyLayerMask);
        return raycastHit2dPlatform.collider != null || raycastHit2dEnemy.collider != null;
    }

    public void ShowInteractableIcon()
    { interactIcon.SetActive(true); }

    public void HideInteractableIcon()
    {
        if (!interactIcon.IsDestroyed())
        interactIcon.SetActive(false);
    }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.IsInteractable())
                {
                    rc.Interact();
                    return;
                }
            }
        }
    }
}
