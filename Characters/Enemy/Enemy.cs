using CombatSystem.Combat;
using CombatSystem.Core;

namespace CombatSystem.Characters;

abstract class Enemy : Character
{
    private List<int> _enemyAttackCountList = new List<int>();
    protected List<EnemyWeapon> _enemyWeapons = new List<EnemyWeapon>();

    #region Enemy Proberties
    public bool IsEnemyMultiplierAllowed => _enemyAttackCountList.Count >= 3;
    #endregion
    
    public Enemy(string name, float health)
        : base(name, health)
    {
    }

    public void AddEnemyWeapon(EnemyWeapon weapon)
    {
        foreach (EnemyWeapon existingWeapon in _enemyWeapons)
            if (existingWeapon.Name == weapon.Name)
                return;

        _enemyWeapons.Add(weapon);
    }

    public void ShowEnemyWeapons()
    {
        foreach (EnemyWeapon weapon in _enemyWeapons)
        {
            Console.WriteLine(weapon.Name);
        }
    }

    public void AddAttackToEnemyAttackList()
    {
        _enemyAttackCountList.Add(0);
    }

    public void ShowEnemyAttackList()
    {
        foreach (int attack in _enemyAttackCountList)
        {
            Console.Write($"{attack} ");
        }
    }

    public void ClearEnemyAttackList()
    {
        _enemyAttackCountList.Clear();
    }

    public override DamageResult TakeDamage(float amount)
    {
        // is dead before damage?
        if (IsDead)
            return DamageResult.AlreadyDead;

        Health -= amount;
        
        // did damage kill?
        if (IsDead)
            return DamageResult.Killed;

        // did survive
        return DamageResult.Survived;
    }

    protected abstract void PerformAttack(IDamageable target);

    public virtual void Attack(IDamageable target)
    {
        if (this.IsDead)
            return;
        
        if (target == this)
            return;

        PerformAttack(target);
    }
}