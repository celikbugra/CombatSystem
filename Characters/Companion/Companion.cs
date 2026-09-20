using CombatSystem.Core;

namespace CombatSystem.Characters;

abstract class Companion : Character
{
    public Companion(string name, float health)
        : base(name, health)
    {
    }

    #region Properties
    public float Heal { get; protected set; }
    public float VampireHeal { get; protected set; }
    public bool CanTurn => Health <= 5 && Health > 0;

    private CompanionState _currentCompanionState;
    public CompanionState CurrentCompanionState
    {
        get
        {
            return _currentCompanionState;
        }

        private set
        {
            _currentCompanionState = value;
        }
    }
    #endregion

    protected void SetCompanionHealth(float value)
    {
        Health = value;
    }

    protected void ChangeCompanionState(CompanionState newState)
    {
        _currentCompanionState = newState;
    }

    public void HealPlayer(Player player)
    {
        if (!player.IsWounded)
            return;

        if (CurrentCompanionState == CompanionState.TurnedIntoVampire)
        {
            player.AddHealth(VampireHeal);
            Console.WriteLine($"{player.Name} wurde um {VampireHeal} geheilt");
        }

        else
        {
            player.AddHealth(Heal);
            Console.WriteLine($"{player.Name} wurde um {Heal} geheilt");
        }
    }

    public override DamageResult TakeDamage(float amount)
    {
        if (IsDead)
            return DamageResult.AlreadyDead;

        Health -= amount;

        if (CanTurn)
            TurnIntoVampire();
            
        if (IsDead)
            return DamageResult.Killed;

        return DamageResult.Survived;
    }

    protected abstract void TurnIntoVampire();
}