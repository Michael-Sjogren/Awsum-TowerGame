using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;
using EffectData;

public class Enemy : MonoBehaviour , IDamagable
{
    [Header("UI")]
    // create delegeate that the hp bar can listen to a damagable and be updated

    [Header("Pathfinding")]
    // TODO move this to an interface?
    public Transform target;
    private MoveAgentToTarget agentMover;
    // status effects
    public List<StatusEffectData> effects;
    // particle effect fire - test
    public Attributes attributes;
    public EnemyData enemyData;
    public bool IsAlive { get; set;}

    public void Start()
    {
        enemyData.Initialize(this);
        agentMover = GetComponent<MoveAgentToTarget>();
        agentMover.target = target;
        agentMover.agent.speed = attributes.moveSpeed;
        // buffs and debuffs
        effects = new List<StatusEffectData>(5);
        agentMover.OnReachedDestination += ReachedGoal;
    }
    // Hurt the player and die
    private void ReachedGoal()
    {
        PlayerManager.Instance.player.TakeDamage(1);
        StartCoroutine(Die());
        Debug.Log("ReachedGoal");
    }

    public void Update()
    {
        UpdateEffects();
         
    }

    // updates debuffs and buffs
    public void UpdateEffects()
    {
        if(effects.Count > 0) 
        {
            for(int i = 0; i < effects.Count; i++) 
            {
                StatusEffectData effect = effects[i];
                effect.UpdateEffect( this , Time.deltaTime );
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if(damage <= 0 ) 
        {
            Debug.Log("Damage value is less or equal to zero");
        }
        attributes.health -= damage;

        if(enemyData.healthChanged != null)
            enemyData.healthChanged();

        if (attributes.health <= 0)
        {
            PlayerManager.Instance.player.ReciveMoney(attributes.dropReward);
            StartCoroutine(Die());
        }
    }
    // Update is called once per frame
    private void Attack(IDamagable target, float amount)
    {
        target.TakeDamage(amount);
    }

    public IEnumerator Die( float delay = 0 )
    {
        EnemySpawner.enemies.Remove(this);
        yield return new WaitForSeconds( delay );
        Destroy(this.gameObject);
    }

    public void AddEffect(StatusEffectData statusEffect)
    {
        if(statusEffect == null) return;

        for (int i = effects.Count - 1; i >= 0 ; i--)
        {
            StatusEffectData effect = effects[i];
            if(statusEffect.name == effect.name)
            {
                Debug.Log("Tried to add existing effect");
                return;
            }
        }
        

        StatusEffectData resultEffect = statusEffect.TryCombiningEffects(this); 
        StatusEffectData newEffect = resultEffect != null ? resultEffect : statusEffect;
        
        effects.Add(newEffect);
        newEffect.Initialize(this);
        UpdateAttributes();
    }

    
    public void RemoveEffect(StatusEffectData statusEffect)
    {
        if(effects.Remove(statusEffect))
        {
            Debug.Log("Removed effect: " + statusEffect.name);
        }
        else 
        {
            Debug.Log("Couldnt remove effect , didnt exist");
        }
        UpdateAttributes();
    }

    void OnDisable()
    {
        agentMover.OnReachedDestination -= ReachedGoal;
      
    }

    private void UpdateAttributes()
    {
        agentMover.agent.speed = attributes.moveSpeed;
    }

}
