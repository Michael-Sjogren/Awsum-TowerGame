
using System.Collections;
using UnityEngine;
namespace Assets.ScriptableObjects.StatusEffects
{
    [CreateAssetMenu(menuName="StatusEffects/Debuffs/Stackable Debuff")]
    public class StackableEffect : TimedEffect
    {
        public int maxStacks;
        public StatModifer modifer;
        public AttributeEnum attriEnum;
        public Ability resultingAbillity;

        public override void BeginEffect(LivingEntity entity)
        {
            var system = entity.GetComponent<StatusEffectSystem>();
            system.AddNewStackEffect(this, maxStacks);
            entity.StartCoroutine(DoEffect(entity));
        }

        public override IEnumerator DoEffect(LivingEntity entity)
        {
            float elapsedTime = 0;
            var stacks = entity.GetComponent<StatusEffectSystem>().GetStacksFromStackEffect(this);
            while (effectLifeTime >= elapsedTime)
            {
                if (stacks.Count >= maxStacks)
                {
                    resultingAbillity.TriggerAbility(entity.gameObject , entity.centerPosition.transform.position );
                    EndEffect(entity);
                    yield break;
                }
                yield return null;
            }

            if (stacks.Count > 0)
            {
                StatModifer modifer = stacks.Pop();
                entity.RemoveStatModifer(modifer, attriEnum);
                float value = entity.GetStat(attriEnum).Value;

                entity.StartCoroutine(DoEffect(entity));
            }
            else
            {
                EndEffect(entity);
            }

            yield return null;
        }

        public override void EndEffect(LivingEntity entity)
        {
            RemoveVisualEffectAndStop(entity);
            RemoveEffect(entity);
        }

    }
}
