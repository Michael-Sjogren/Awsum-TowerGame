
using System;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour 
{
    public Image healthBar;
    public Enemy entity;
    void Start()
    {
        entity.enemyData.healthChanged += UpdateHealth;
    }
    public void UpdateHealth()
    {
        healthBar.fillAmount = entity.attributes.health / entity.attributes.maxHealth;
    }

    void OnDisable()
    {
        entity.enemyData.healthChanged -= UpdateHealth;
    }
}