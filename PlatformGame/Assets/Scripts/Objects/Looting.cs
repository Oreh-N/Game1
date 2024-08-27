using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


[RequireComponent(typeof(SpriteRenderer))]   // we know for a fact that we will always have SpriteRenderer
public class Looting : Interactable
{
    public Loot[] items;
    float rnd;

    public float xDiff = 1f;
    public float yDiff = 0.5f;

    private bool isOpen;

    private SpriteRenderer sr;
    public Sprite open;
    public Sprite close;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = close;
        isOpen = false;
    }

    public override void Interact()
    {
        float distBtwItems = 0.5f;

        if (!isOpen)
        {
            sr.sprite = open;
            isOpen = !isOpen;
        }
        else { return; } //sr.sprite = close;
            
        for (int i = 0; i < items.Length; i++)
        {
            rnd = Random.Range(0, 100);

            if (rnd <= items[i].dropChance)
            {
                Vector2 lootPos = new Vector2(transform.position.x + xDiff + distBtwItems*i, transform.position.y - yDiff);
                Instantiate(items[i].item, lootPos, Quaternion.identity);
            }
        }
    }
}
