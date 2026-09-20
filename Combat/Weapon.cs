namespace CombatSystem.Combat;

abstract class Weapon
{
    public Weapon(string name, float damage)
    {
        Name = name;
        Damage = damage;
    }

    public string Name { get; protected set; }

    public float Damage{ get; protected set; }

    public float Multiplier { get; protected set; }
}


class PlayerWeapon : Weapon
{
    public PlayerWeapon(string name, float damage)
        : base(name, damage)
    {
    }
}

class EnemyWeapon : Weapon
{
    public EnemyWeapon(string name, float damage)
        : base(name, damage)
    {
    }

    public EnemyWeapon(string name, float damage, float multiplier)
        : base(name, damage)
    {
        Multiplier = multiplier;
    }
}

class VampireFangs : EnemyWeapon
{
    public VampireFangs(string name, float damage, float multiplier)
        : base(name, damage, multiplier)
    {
    }
}