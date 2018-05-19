[System.Serializable]
public struct Cooldown
{
    public readonly string effectName;
    private readonly Enemy enemy;
    public float elapsed;
    public float duration;
    public Cooldown(float duration , string effectName ,  Enemy e)
    {
        this.duration = duration;
        this.effectName = effectName;
        this.enemy = e;
        this.elapsed = 0f;
    }


    public void UpdateCooldown(float deltaTime)
    {
        elapsed += deltaTime;
        if(elapsed >= duration)
        {
            enemy.cooldowns.Remove(this);
        }
    }

    public void ResetTime()
    {
        elapsed = 0f;
    }
}