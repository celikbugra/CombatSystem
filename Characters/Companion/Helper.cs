using CombatSystem.Core;

namespace CombatSystem.Characters;

class Helper : Companion
{
    private const float StartingHealth = 25f;
    private const float DefaultHeal = 15f;
    private const float DefaultVampireHeal = 50f;
    private const float VampireHealth = 100f;

    public Helper(float heal, float vampireHeal)
        : base("Mina", StartingHealth)
    {
        Heal = heal;
        VampireHeal = vampireHeal;
    }

    public Helper()
        : this(DefaultHeal, DefaultVampireHeal)
    {
    }

    protected override void TurnIntoVampire()
    {
        if (CanTurn && CurrentCompanionState != CompanionState.TurnedIntoVampire)
        {
            ChangeCompanionState(CompanionState.TurnedIntoVampire);
            SetCompanionHealth(VampireHealth);

            Console.WriteLine($"{this.Name} turned into Vampire" + 
                              $" and set Health to {this.Health}");
        }
    }
}