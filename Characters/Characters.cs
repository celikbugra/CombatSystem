abstract class Character : IDamageable
{
    public Character(string name, float health)
    {
        Name = name;
        Health = health;
    }

    public string Name { get; protected set; }
    private float _health;
    public float Health 
    {
        get
        {
            return _health;
        }

        protected set
        {
            _health = MathF.Max(0, value);
        }
    }
    
    public virtual bool IsDead => Health <= 0;

    public abstract DamageResult TakeDamage(float amount);
}