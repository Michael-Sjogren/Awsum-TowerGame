
using System.Collections;
using UnityEngine;

// a living entity can die and move
public abstract class LivingEntity : Entity , IDamagable  , IMoveable
{
    public bool IsAlive { get { return isAlive;} set{ isAlive = value;}}
    [HideInInspector]
    private float health;
    private float maxHealth;
    private float movementSpeed;
    private bool isAlive = true;
    public float Health {get {return health;} set { health = value; } }
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }
    public AgentController Controller {get;set;}

    public float MovementSpeed { get {return movementSpeed; } set{movementSpeed = value;} }

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
        if(Health - amount <= 0) 
        {
            Health = 0;
            IsAlive = false;
            StartCoroutine(Die(0));
            OnHealthChanged();
            return;
        }
        Health -= amount; 
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