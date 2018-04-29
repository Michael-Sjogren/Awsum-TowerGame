

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIUnitSelectionHealth : UIUnitSelection
{
    public TextMeshProUGUI healthText;
    public Image healthImage;

    private float health = 0;
    private float maxHealth = 0;

    [Range(0f , 1f)]
    [SerializeField]
    private float lerpAmount = .25f;

    private LivingEntity currentEntity;
    public override void UpdateUI(LivingEntity entity)
    {
        healthImage.fillAmount = 1f;
        currentEntity = entity; 
        if(currentEntity != null) 
        {
            health = currentEntity.Health;
            maxHealth = currentEntity.MaxHealth;
            float fillAmount = health / maxHealth;
            healthImage.fillAmount = fillAmount;
            healthText.SetText( (int) health + " / " + (int) maxHealth);
        }
        else
        {
            healthText.SetText("???");
        }     
    }

    void Update()
    {
        if(currentEntity != null) 
        {
            health = currentEntity.Health;
            maxHealth = currentEntity.MaxHealth;
            
            healthImage.fillAmount = health / maxHealth;
            string s = (int) health + " / " + (int) maxHealth;
            healthText.SetText(s);  
        }
    }
}