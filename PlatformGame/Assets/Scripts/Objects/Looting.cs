using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


[RequireComponent(typeof(SpriteRenderer))]   // we know for a fact that we will always have SpriteRenderer
public class Looting : Interactable
{
    private LootSpawn spawn;

    private SpriteRenderer sr;
    private Animator anim;
    public Sprite open;
    public Sprite close;
    private bool isOpen;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        spawn = GetComponent<LootSpawn>();
        anim = GetComponent<Animator>();
        sr.sprite = close;
        isOpen = false;
    }

    public override void Interact()
    {
        if (!isOpen)
        {
            anim.SetTrigger("open");
            //sr.sprite = open;
            isOpen = true;
        }
        else { return; } //sr.sprite = close;

        spawn.SpawnLoot();
    }
}
