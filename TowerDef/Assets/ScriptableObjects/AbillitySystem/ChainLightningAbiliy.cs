using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Abilities/Chain Lightning")]
public class ChainLightningAbiliy : Ability
{
    public string targetTag;
    public int maxBounces;
    public float radius;
    public float bounceDamage;
    public float percentageFallout;

    public override void PlayVisualEffect(VisualEffect effect)
    {

        if (effect != null)
        {
            effect.Play(true);
        }
    }

    public override void StopVisualEffect(VisualEffect effect)
    {
        if (effect != null)
        {
            effect.Stop(0 , ParticleSystemStopAction.Destroy);
        }
    }

    public override void TriggerAbility(GameObject obj, Vector3 center)
    {

        var targetsToHit = new List<GameObject>(maxBounces);
        var targets = new List<LivingEntity>(maxBounces);

        var originalEntity = obj.GetComponent<LivingEntity>();
        if (originalEntity == null) return;
        var originalGameObject = originalEntity.centerPosition;
        Vector3 pos = center;
        targetsToHit.Add(originalGameObject);

        for (int i = 1; i <= maxBounces; i++)
        {
            var colliders = Physics.OverlapSphere(pos, radius);
            var closestObject = GetClosestTarget(colliders, targetsToHit, originalGameObject);
            if (closestObject != null)
            {
                var targetEntity = closestObject.GetComponent<LivingEntity>();
                if (targetEntity != null)
                {
                    var targetPos = targetEntity.centerPosition.transform.position;
                    pos = targetEntity.centerPosition.transform.position;
                    targetsToHit.Add(targetEntity.centerPosition);
                    targets.Add(targetEntity);
                }
            }
            else
            {
                break;
            }
        }


        var lightningEffect = Instantiate(abillityVisualEffect, center, Quaternion.identity) as LightningVisualEffect;
        lightningEffect.targets = targetsToHit;
        lightningEffect.Play(true);

        for (int i = 0; i < targets.Count; i++)
        {
            var targetEntity = targets[i];
            float damage = bounceDamage * (100 - (i * percentageFallout)) / 100;
            targetEntity.TakeDamage(damage);
        }
    }

    private GameObject GetClosestTarget(Collider[] colliders, List<GameObject> targetsToHit, GameObject origin)
    {
        GameObject min = null;
        float minDist = radius;
        foreach (Collider c in colliders)
        {
            if (!c.gameObject.CompareTag(targetTag))
            {
                continue;
            }

            if (c == origin)
            {
                continue;
            }
            var entity = c.gameObject.GetComponent<LivingEntity>();
            if (entity == null)
            {
                continue;
            }

            if (targetsToHit.Contains(entity.centerPosition) || entity.centerPosition == origin)
            {
                continue;
            }

            float dist = Vector3.Distance(entity.centerPosition.transform.position, origin.transform.position);
            if (dist < minDist)
            {
                min = entity.gameObject;
                minDist = dist;
            }
        }
        return min;
    }

    private GameObject CreateEmptyGameObjectFromPos(Vector3 pos)
    {
        var obj = new GameObject();
        obj.transform.position = pos;
        return obj;
    }
}
