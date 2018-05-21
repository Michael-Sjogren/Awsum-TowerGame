
using System.Collections;
using UnityEngine;

// a living entity can die and move
public abstract class LivingEntity : Entity , IDamagable  , IMoveable
{
    public bool IsAlive { get { return isAlive;} set{ isAlive = value;}}
    [HideInInspector]

    private float maxHealth;
    private bool isAlive = true;
    [HideInInspector]
    public float Health {get; set;}
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value;} }
    public AgentController Controller {get;set;}
    [HideInInspector]
    public Stat MovementSpeed;

    
    public virtual IEnumerator Die(float delay)
    {
       yield return new WaitForSeconds(delay);
       Destroy(this.gameObject);
    }

    public virtual void TakeDamage(float amount)
    {
        if(Health - amount <= 0) 
        {
            IsAlive = false;
            StartCoroutine(Die(0));
            OnStatChanged();
            return;
        }
        Health -= amount;
        OnStatChanged();
    }

    public virtual void MoveTo(Vector3 position)
    {
        Controller.Move(position);
    }

    public override void Initialize()
    {
        IsAlive = true;
        Controller = GetComponent<AgentController>();
    }

    public float GetHealth()
    {
        return Health;
    }

    public Stat GetMovementSpeed()
    {
        return MovementSpeed;
    }

    public void UpdateStats()
    {
        OnStatChanged();
    }
}