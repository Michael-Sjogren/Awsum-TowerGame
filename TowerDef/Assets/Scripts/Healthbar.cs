
using System;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour 
{
    public Image healthBar;
    public LivingEntity entity;
    void Start()
    {
        entity.OnHealthChanged += UpdateHealth;
    }
    public void UpdateHealth()
    {
        healthBar.fillAmount = entity.Health / entity.MaxHealth;
    }

    void OnDisable()
    {
        entity.OnHealthChanged -= UpdateHealth;
    }
}