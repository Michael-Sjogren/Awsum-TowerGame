using System;

public interface IDamagable 
{
    bool IsAlive { get; set; }
    void TakeDamage(float amount);
}