using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


[RequireComponent(typeof(SpriteRenderer))]   // we know for a fact that we will always have SpriteRenderer
public class Looting : Interactable
{
    private LootSpawn spawn;

    private SpriteRenderer sr;
    public Sprite open;
    public Sprite close;
    private bool isOpen;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        spawn = GetComponent<LootSpawn>();
        sr.sprite = close;
        isOpen = false;
    }

    public override void Interact()
    {
        if (!isOpen)
        {
            sr.sprite = open;
            isOpen = !isOpen;
        }
        else { return; } //sr.sprite = close;

        spawn.SpawnLoot();
    }
}
