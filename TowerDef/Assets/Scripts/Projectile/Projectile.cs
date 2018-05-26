using Buildings;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour  
{
    public ProjectileData projectileData;
    private GameObject target;
    private float speed;
    private Vector3 lastTargetPos;
    private float damage;
    private bool hasInitialized = false;
    private ParticleSystem projectileEffect;

    void Update()
    {
        if(hasInitialized) 
        {
            MoveToTarget();
        }
    }

    public void SetTarget( GameObject target , float damage )
    {
        this.target = target;
        this.damage = damage;
        Intitialize();
    }

    private void Intitialize()
    {
        speed = projectileData.speed;
        hasInitialized = true;

        projectileEffect = (Instantiate(projectileData.projectileEffect));
        projectileEffect.transform.SetParent(this.transform);
        projectileEffect.transform.localPosition = Vector3.zero;
        projectileEffect.Play();
    }

    void MoveToTarget()
    {
        if(target != null) 
        {
            lastTargetPos = target.transform.position;
        }

        Vector3 dir =   lastTargetPos - transform.position;
        float distThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distThisFrame) 
        {
            if(target != null) 
            {
                Enemy enemy = target.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
                if(projectileData.effectData != null) 
                {
                    enemy.AddStatusEffect(projectileData.effectData);
                }
                projectileEffect.Stop(true , ParticleSystemStopBehavior.StopEmittingAndClear );
            }
            Destroy(this.gameObject);
            return;
        } 
        this.transform.rotation = Quaternion.LookRotation(dir , transform.up);
        this.transform.Translate(dir.normalized * distThisFrame , Space.World );
    }

}
