namespace CombatSystem.Core;

public enum PlayerState
{
    None,
    Idle,
    Attacking,
    Rage,
    Hurt,
    Dead
}

public enum EnemyState
{
    None,
    Idle,
    Attacking,
    Rage,
    Hurt,
    Dead
}

public enum DamageResult
{
    None,
    AlreadyDead,
    Killed,
    Survived
}

public enum HealthState
{
    None,
    Healthy,
    Wounded,
    Critical,
    Dead
}

public enum StatusEffect
{
    None,
    Bleeding
}

public enum CompanionState
{
    None,
    Normal,
    TurnedIntoVampire
}

public enum AttackerType
{
    Player,
    Enemy
}