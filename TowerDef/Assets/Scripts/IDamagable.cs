using System;
using System.Collections;

public interface IDamagable 
{
    bool IsAlive { get; set; }
    float Health { get; set;}
    float MaxHealth{ get; set; }
    IEnumerator Die(float delay);
    void TakeDamage(float amount);
}