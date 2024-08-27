using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public PlayerController player;
    public Slider health;

    void Start()
    {
        health.maxValue = player.health.maxHealth;
        health.value = player.health.currHealth;
    }

    void Update()
    { health.value = player.health.currHealth; }
}
