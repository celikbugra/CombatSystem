using System.Threading;
using CombatSystem.Combat;
using CombatSystem.Core;

namespace CombatSystem.Characters;

class Vampire : Enemy
{
    private EnemyWeapon _claws;
    private VampireFangs _fangs;

    private const float StartingHealth = 300f;
    private const float ClawDamage = 23f;
    private const float FangDamage = 25f;
    private const float FangDamageMultiplier = 1.75f;

    public Vampire()
        : base("Regis", StartingHealth)
    {
        _claws = new EnemyWeapon("Claws",  ClawDamage);
        _fangs = new VampireFangs("Fangs", FangDamage, FangDamageMultiplier);

        _enemyWeapons.Add(_claws);
        // ! _enemyWeapons.Add(_fangs); ! //
    }

    protected virtual void ClawAttack(IDamageable target)
    {
        string weaponName = _claws.Name;

        float clawAttackDamage  = _claws.Damage;

        target.TakeDamage(clawAttackDamage);

        Console.WriteLine($"{this.Name} dealt {clawAttackDamage} damage with: {weaponName}");
    }

    protected virtual void FangAttack(IDamageable target)
    {
        string weaponName    = _fangs.Name;

        float totalDamage      = 0;
        float fangAttackDamage = 0;

        float currentMultiplier = FangDamageMultiplier;

        // damage calculation
        for (int i = 0; i < 3; i++)
        {
            fangAttackDamage = _fangs.Damage * FangDamageMultiplier;

            target.TakeDamage(fangAttackDamage);

            totalDamage += fangAttackDamage;

            currentMultiplier *= 0.75f;

            Thread.Sleep(500);
        }

        Console.WriteLine($"{this.Name} dealt {totalDamage} damage with: {weaponName}");
    }

    protected override void PerformAttack(IDamageable target)
    {
        int attack = Random.Shared.Next(_enemyWeapons.Count);

        if (attack == 0)
            ClawAttack(target);

        else
            FangAttack(target);

        AddAttackToEnemyAttackList();
        Config.AddAttackToAttackHistory(AttackerType.Enemy);

        if (target is Player player)
            player.ClearPlayerAttackList();
    }
}