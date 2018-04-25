using Buildings;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour 
{
    private GameObject target;
    private float projectileDmg;
    private float speed;
    private ParticleSystem projectileEffect;
    public ProjectileData projectileData;
    public void SetTarget(GameObject t )
    {
        projectileDmg = projectileData.damage;
        speed = projectileData.speed;
        projectileEffect = Instantiate(projectileData.projectileEffect);
        projectileEffect.transform.SetParent(this.transform);
        projectileEffect.transform.localPosition = Vector3.zero;
        projectileEffect.transform.rotation = this.transform.rotation;
        projectileEffect.Play(true);
        target = t;
    }
    void Update()
    {
        if(target == null) { 
            Destroy(this.gameObject); 
            return; 
        }
        
        Vector3 dir =   target.transform.position - transform.position ;
        float distThisFrame = speed * Time.deltaTime;
        if(dir.magnitude <= distThisFrame) 
        {
            Enemy enemy = target.GetComponent<Enemy>();
            enemy.TakeDamage(projectileDmg);
            enemy.AddEffect(projectileData.effectData);
            projectileEffect.Stop(true , ParticleSystemStopBehavior.StopEmittingAndClear);
            Destroy(this.gameObject);
            return;
        }
        
        this.transform.rotation = Quaternion.LookRotation(dir , transform.up);
        this.transform.Translate(dir.normalized * distThisFrame , Space.World );
    }

}
