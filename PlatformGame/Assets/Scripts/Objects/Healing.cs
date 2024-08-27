using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;


public class Healing : MonoBehaviour, IUsable
{
    public PlayerController player;
    public int heal;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void Use()
    { player.health.Heal(heal); }
}
