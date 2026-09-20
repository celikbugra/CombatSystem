using CombatSystem.Combat;
using CombatSystem.Core;

namespace CombatSystem.Characters;

class Geralt : Player
{
    private PlayerWeapon _silverSword;
    private PlayerWeapon _steelSword;

    private const float StartingHealth = 250f;
    private const float SilverSwordDamage = 25f;
    private const float SteelSwordDamage = 10f;

    public Geralt()
        : base("Geralt", StartingHealth)
    {
        _silverSword = new PlayerWeapon("Silver Sword", SilverSwordDamage);
        _steelSword = new PlayerWeapon("Steel Sword",    SteelSwordDamage);

        AddWeapon(_silverSword);
        AddWeapon(_steelSword);

        Companion = new Helper();
    }

    protected virtual void SilverSwordAttack(IDamageable target)
    {
        string weaponName = _silverSword.Name;

        float silverAttackDamage = _silverSword.Damage;

        if (IsPlayerMultiplierAllowed)
            silverAttackDamage *= 2;

        target.TakeDamage(silverAttackDamage);

        Console.WriteLine($"{this.Name} dealt {silverAttackDamage} damage " +
                          $"with: {weaponName}");
    }

    protected virtual void SteelSwordAttack(IDamageable target)
    {
        string weaponName = _steelSword.Name;

        float steelDamage = _steelSword.Damage;
        float steelAttackDamage = steelDamage;

        if (IsPlayerMultiplierAllowed)
            steelAttackDamage *= 2;

        target.TakeDamage(steelAttackDamage);

        Console.WriteLine($"{this.Name} dealt {steelAttackDamage} damage" +
                          $"with: {weaponName}");
    }

    protected override void PerformAttack(IDamageable target)
    {
        int attack = Random.Shared.Next(_playerWeapons.Count);

        if (attack == 0)
            SilverSwordAttack(target);

        else
            SteelSwordAttack(target);

        AddAttackToPlayerAttackList();
        Config.AddAttackToAttackHistory(AttackerType.Player);
    }
}