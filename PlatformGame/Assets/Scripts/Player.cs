using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject interactIcon;

    [SerializeField] public float jumpForce = 11f;
    [SerializeField] public int defence = 5;
    [SerializeField] public int power = 10;
    [SerializeField] public float speed = 10;
    [SerializeField] public int health = 100;
    [SerializeField] public int level = 1;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private BoxCollider2D boxCollider2d;
    [SerializeField] private LayerMask platformsLayerMask;    
    private Vector2 boxSize = new Vector2 (0.1f, 1f);



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
        rb = transform.GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            Jump();
        if (Input.GetButton("Horizontal"))
            Move();
        if (Input.GetKeyDown(KeyCode.E))
            CheckInteraction();
    }

    private void Move()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position += speed * dir * Time.deltaTime;
        sprite.flipX = dir.x < 0.0f;

    }

    private void Jump()
    { rb.velocity = Vector2.up * jumpForce; }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, platformsLayerMask);
        return raycastHit2d.collider != null;
    }

    public void ShowInteractableIcon()
    { interactIcon.SetActive(true); }

    public void HideInteractableIcon()
    { interactIcon.SetActive(false); }

    private void CheckInteraction()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, boxSize, 0, Vector2.zero);

        if (hits.Length > 0)
        {
            foreach (RaycastHit2D rc in hits)
            {
                if (rc.IsInteractable())
                {
                    Debug.Log("Problem is down here 3");
                    rc.Interact();
                    return;
                }
            }
        }
    }
}
