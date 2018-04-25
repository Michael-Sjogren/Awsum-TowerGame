public class Cooldown
{
    private readonly string effectName;
    private readonly Enemy enemy;
    public float elapsed = 0f;
    public float duration = 0;
    public Cooldown(float duration , string effectName ,  Enemy e)
    {
        this.duration = duration;
        this.effectName = effectName;
        this.enemy = e;
    }


    public void UpdateCooldown(float deltaTime)
    {
        elapsed += deltaTime;
        if(elapsed >= duration)
        {
            enemy.coolDowns.Remove(effectName);
        }
    }

    public void ResetTime()
    {
        elapsed = 0f;
    }


}