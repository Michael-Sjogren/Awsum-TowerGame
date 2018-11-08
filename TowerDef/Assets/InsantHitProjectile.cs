

using UnityEngine;

public class BeamProjectile : Projectile
{
    public GameObject startPoint;

    public override void Fire()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        enemy.TakeDamage(damage);

        if (projectileData.ability != null)
        {
            projectileData.ability.TriggerAbility(enemy.gameObject, enemy.centerPosition.transform.position);
        }

        if (projectileData.hitSound != null)
        {
            projectileData.hitSound.Play(target.GetComponent<AudioSource>());
        }

      
    }

    public override void OnTargetHit()
    {
        
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
