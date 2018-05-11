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
    
    private Vector3 lastTargetPos;
    public void Launch(GameObject t , Tower owner )
    {
        projectileDmg = owner.Damage.Value;
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

        if(target != null)
            lastTargetPos = target.transform.position;
        Vector3 dir =   lastTargetPos - transform.position;
        float distThisFrame = speed * Time.deltaTime;
        if(dir.magnitude <= distThisFrame) 
        {
            if(target != null) 
            {
                Enemy enemy = target.GetComponent<Enemy>();
                enemy.TakeDamage(projectileDmg);
                enemy.AddEffect(projectileData.effectData);
            }
            projectileEffect.Stop(true , ParticleSystemStopBehavior.StopEmittingAndClear);
           
            Destroy(this.gameObject);
            return;
        }
        
        this.transform.rotation = Quaternion.LookRotation(dir , transform.up);
        this.transform.Translate(dir.normalized * distThisFrame , Space.World );
    }

}
