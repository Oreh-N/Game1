using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    

    public void InitializeHealthBar(int maxHealth, int currHealth)
    {
        slider.maxValue = maxHealth; 
        slider.value = currHealth;
    }

    public void UpdateHealthBar(int currHealth)
    { slider.value = currHealth; }
}
