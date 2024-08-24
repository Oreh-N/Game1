using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Healing : Interactable
{
    public PlayerController player;
    private int heal;


    void Start()
    { player = GetComponent<PlayerController>(); }


    public override void Interact()
    { player.health += heal; }
}
