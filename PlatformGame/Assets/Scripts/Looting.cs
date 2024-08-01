using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]   // we know for a fact that we will always have SpriteRenderer
public class Looting : Interactable
{
    public Sprite open;
    public Sprite close;

    private SpriteRenderer sr;
    private bool isOpen;


    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = close;
    }

    public override void Interact()
    {
        if (isOpen)
            sr.sprite = close;
        else 
            sr.sprite = open;
        
        isOpen = !isOpen;
    }
}
