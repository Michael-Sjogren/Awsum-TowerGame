
using UnityEngine;

namespace EffectData {
    [CreateAssetMenu(fileName="New Area Damage Effect" , menuName ="StatusEffects/Debuffs/New Area Damage Effect")]
    public class AreaDamageData : DamageEffectData 
    {
        public float radius;

        public override void BeginEffect(Enemy e , ParticleSystem particleSystem )
        {
            base.BeginEffect(e , particleSystem);
            
            Vector3 pos = e.transform.position;
            Collider[] colliders = Physics.OverlapSphere( pos , radius );
            if(colliders.Length > 0) 
            {
                foreach(Collider c  in colliders) 
                {
                    Enemy enemy = c.transform.GetComponent<Enemy>();
                    if(enemy == null) 
                    {
                        continue;
                    } 
                    if(enemy == e) 
                    {
                        continue;
                    } 
                    enemy.TakeDamage(damage); 
                }
            }
            EndEffect(e);
        }

        public override void EndEffect(Enemy e)
        {
            Debug.Log("Ending effect");
            RemoveSelf(e);
        }

        public override void UpdateEffect(Enemy e , float deltaTime )
        {
            
        }
    }
}