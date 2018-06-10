using Buildings;
using UnityEngine;
using System;

public class HomingProjectile : Projectile  
{
    private float speed;
    private Vector3 lastTargetPos;
    private bool hasFired = false;

    void Update()
    {
        if(hasFired) 
        {
            MoveToTarget();
        }
    }

    public override void Fire()
    {
        var data = projectileData as SimpleProjectileData;
        speed = data.speed;
        hasFired = true;

        projectileEffect = (Instantiate(data.projectileEffect));
        projectileEffect.transform.SetParent(transform);
        projectileEffect.transform.localPosition = Vector3.zero;
        projectileEffect.Play();
    }

    void MoveToTarget()
    {
        if(target != null) 
        {
            lastTargetPos = target.centerPosition.transform.position;
        }

        Vector3 dir =   lastTargetPos - transform.position;
        float distThisFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distThisFrame) 
        {
            if(target != null) 
            {
                OnTargetHit();
            }
            Destroy(this.gameObject);
            return;
        } 
        this.transform.rotation = Quaternion.LookRotation(dir , transform.up);
        this.transform.Translate(dir.normalized * distThisFrame , Space.World );
    }

    public override void OnTargetHit()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        enemy.TakeDamage(damage);
        if (projectileData.ability != null)
        {
            projectileData.ability.TriggerAbility(enemy.gameObject, enemy.centerPosition.transform.position);
        }

        StartCoroutine(projectileEffect.Stop(0));
    }
}
