
using System.Collections;

using UnityEngine;


public class Enemy : LivingEntity
{
    // create delegeate that the hp bar can listen to a damagable and be updated

    [Header("Enemy")]
    // TODO move this to an interface?
    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public bool reachedGoal = false;
    [HideInInspector]
    public int goldDropReward;
    private AudioSource source;
    public SimpleAudioEvent coinDropSound;

    public override void Start()
    {
        base.Start();
        source = GetComponent<AudioSource>();
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
        }
    }

    private void Attack(IDamagable target, float amount)
    {
        target.TakeDamage(amount);
    }

    private new void Update()
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
            PlayerManager.Instance.player.ReciveMoney(goldDropReward);
            coinDropSound.Play(PlayerManager.Instance.player.GetComponent<AudioSource>());
        }
        GameManager.instance.RemoveEnemy(this);
        Destroy(this.gameObject);
    }
    private void OnDisable()
    {
        Controller.OnReachedDestination -= ReachedGoal;
    }
}
