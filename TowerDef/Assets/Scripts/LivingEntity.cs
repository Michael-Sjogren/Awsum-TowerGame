
using System.Collections;
using UnityEngine;

// a living entity can die and move
public abstract class LivingEntity : Entity , IDamagable  , IMoveable
{
    public bool IsAlive { get; set; }
    [HideInInspector]
    public float health;
    public float Health {get {return health;} set { health = value; } }
    public float MaxHealth { get; set; }
    public AgentController Controller {get;set;}

    public float MovementSpeed { get; set; }

    public delegate void AttributesChanged();

    public event AttributesChanged OnAttributeChanged = delegate{};
    public event AttributesChanged OnHealthChanged = delegate{};

    public virtual IEnumerator Die(float delay)
    {
       yield return new WaitForSeconds(delay);
       Destroy(this.gameObject);
    }

    public virtual void TakeDamage(float amount)
    {
        IsAlive = false;
        Health -= amount;
        if(Health <= 0) 
        {
            StartCoroutine(Die(0));
        } 
        OnHealthChanged();
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
}