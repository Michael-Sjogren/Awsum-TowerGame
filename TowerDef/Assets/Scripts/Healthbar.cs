
using System;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour 
{
    public Image healthBar;
    public LivingEntity entity;
    [SerializeField]
    private GameObject healthbarContainer;

    void Start()
    {
        entity.OnStatChanged += UpdateHealth;
        healthbarContainer.SetActive(false);
    }

    public void UpdateHealth()
    {
        if(entity.Health < entity.MaxHealth)
        {
            if(!healthbarContainer.activeSelf)
            {
                healthbarContainer.SetActive(true);
            }
        }

        healthBar.fillAmount = entity.Health / entity.MaxHealth;
    }

    void OnDisable()
    {
        entity.OnStatChanged -= UpdateHealth;
    }
}