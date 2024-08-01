using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Player player;
    public Slider health;

    void Start()
    {
        health.minValue = 0;
        health.maxValue = player.health;

        health.value = player.health;
    }

    void Update()
    { health.value = player.health; }

    public void Damage(int damage)
    { health.value -= damage; }

    public void Heal(int heal)
    { health.value += heal; }
}
