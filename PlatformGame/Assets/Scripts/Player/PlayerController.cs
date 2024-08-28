using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController  : MonoBehaviour
{
    public PlayerHealth health;

    private float dashCooldown = 1f;
    private float dashPower = 2f;
    private float dashTime = 0.1f;
    public float jumpForce = 11f;
    public float speed = 5f;
    public int damage = 10;
    public int defence = 5;
    public int level = 1;
    private int coins = 0;

    private bool goingRight;
    private bool isDashing;
    private bool canDash = true;
    
    [SerializeField] private LayerMask obstaclesLayerMask;
    private Vector2 boxSize = new Vector2 (1.5f, 1f);
    [SerializeField] private TrailRenderer trailRend;
    public static PlayerController Instance;
    private BoxCollider2D boxCollider2d;
    public GameObject interactIcon;
    private Rigidbody2D rb;
    private Animator anim;
    public Text coinsCount;
    public Text lvlText;


    public void SavePlayerState()
    { SavingSystem.SavePlayerState(this); }

    public void LoadPlayerState()
    {
        PlayerData data = SavingSystem.LoadPlayerState();

        level = data.level;
        health.currHealth = data.health;
        speed = data.speed;
        damage = data.power;
        defence = data.defence;

        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;
    }

    private void Awake()
    { Instance = this; }

    void Start()
    {
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        rb = transform.GetComponent<Rigidbody2D>();
        health = transform.GetComponent<PlayerHealth>();
        anim = transform.GetComponent<Animator>();
        SavePlayerState();
    }

    void Update()
    {
        if (isDashing) return;

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
            else if (!goingRight && Input.GetAxis("Horizontal") < 0)
                Flip();
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
                StartCoroutine(Dash());
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
        RaycastHit2D raycastHit2dPlatform = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, obstaclesLayerMask);
        return raycastHit2dPlatform.collider != null;
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

    public void UpdateLevel()
    {
        level++;
        lvlText.text = level.ToString();
    }

    public void UpdateCoinCount(int value)
    {
        coins += value;
        coinsCount.text = coins.ToString();
    }

    public Animator GetAnimator()
    { return anim; }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
        trailRend.emitting = true;
        yield return new WaitForSeconds(dashTime);
        trailRend.emitting = false;
        rb.gravityScale = originGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
