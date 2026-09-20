namespace CombatSystem.Core;

interface IDamageable
{
    public DamageResult TakeDamage(float amount);
}