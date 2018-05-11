using System;
using System.Collections;

public interface IDamagable 
{
    bool IsAlive { get; set; }
    float GetHealth();
    float MaxHealth{ get; set; }
    IEnumerator Die(float delay);
    void TakeDamage(float amount);
}