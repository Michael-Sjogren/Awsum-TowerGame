[System.Serializable]
public struct Cooldown
{
    public readonly string effectName;

    public float elapsed;
    public float duration;

    public Cooldown(float duration , string effectName)
    {
        this.duration = duration;
        this.effectName = effectName;
        this.elapsed = 0f;
    }


    public void UpdateCooldown(float deltaTime,  StatusEffectSystem system)
    {
        elapsed += deltaTime;
        if(elapsed >= duration)
        {
            system.Cooldowns.Remove(this);
        }
    }

    public void ResetTime()
    {
        elapsed = 0f;
    }
}