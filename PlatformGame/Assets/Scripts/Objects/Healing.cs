using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;


public class Healing : MonoBehaviour, IUsable
{
    public PlayerHealth playerHealth;
    public int heal;


    public void Use()
    {
        if (playerHealth != null)
        {
            playerHealth.Heal(heal);
            Debug.Log($"I am healing: +{heal}");
        }
        else
        {
            Debug.LogError($"Player: {playerHealth} or Player's health: {playerHealth} is not assigned!") ;
        }
    }
}
