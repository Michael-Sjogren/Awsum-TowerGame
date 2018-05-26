using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.ScriptableObjects.StatusEffectData;
using Effects;
using ScriptableObjects.Enums;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : LivingEntity
{
    // create delegeate that the hp bar can listen to a damagable and be updated

    [Header("Enemy")]
    // TODO move this to an interface?
    [HideInInspector]
    public Transform target;
    public GameObject coinPrefab;
     [HideInInspector]
    public bool reachedGoal = false;
     [HideInInspector]
    public int goldDropReward;

    public override void Start()
    {
        base.Start();
        
        var data = UnitData as EnemyData;
        data.Initialize(this);

        Controller.OnReachedDestination += ReachedGoal;
    }
    // Hurt the player and die
    private void ReachedGoal()
    {
        if(!reachedGoal) 
        {   
            reachedGoal = true;
            StartCoroutine(Die(0));
            PlayerManager.Instance.player.TakeDamage(1);
            Debug.Log("ReachedGoal");
        }
    }

    private void Attack(IDamagable target, float amount)
    {
        target.TakeDamage(amount);
    }

    new void Update()
    {
        if(target != null)
            MoveTo(target.position);
    }
    public override IEnumerator Die(float delay)
    {
        IsAlive = false;
        yield return new WaitForSeconds(delay);
        
        if(!reachedGoal) 
        {
            for(int i = 0; i < goldDropReward; i++) 
            {
                Instantiate( coinPrefab , this.transform.position + Vector3.up * 1.5f , Quaternion.identity );
            }
            PlayerManager.Instance.player.ReciveMoney(goldDropReward);
        }
        Destroy(this.gameObject);
        spawner.Remove(this);
    }
    private void OnDisable()
    {
        Debug.Log(Controller);
        Controller.OnReachedDestination -= ReachedGoal;
    }
}
