using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public PlayerController player;
    public int maxHealth = 100;
    public int currHealth;

    private void Start()
    {
        player = GetComponent<PlayerController>();
        currHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currHealth -= damage;
        player.GetAnimator().SetTrigger("takeDamage");

        if (currHealth < 0)
        { currHealth = 0; }
    }

    public void Heal(int heal)
    {
        currHealth += heal;
        player.GetAnimator().SetTrigger("heal");

        if (currHealth > 100) 
        { currHealth = 100; }
    }
}
