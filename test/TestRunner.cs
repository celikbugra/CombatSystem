using System;
using System.IO;
using System.Linq;

static class TestRunner
{
    public static void RunAllTests()
    {
        int passed = 0;
        int failed = 0;


        void Section(string name)
        {
            Console.WriteLine();
            Console.WriteLine($"========== {name} ==========");
        }


        void Check(string name, bool condition)
        {
            if (condition)
            {
                passed++;
                Console.WriteLine($"[PASS] {name}");
            }
            else
            {
                failed++;
                Console.WriteLine($"[FAIL] {name}");
            }
        }


        string CaptureOutput(Action action)
        {
            TextWriter originalOutput = Console.Out;
            StringWriter writer = new StringWriter();

            try
            {
                Console.SetOut(writer);
                action();
            }
            finally
            {
                Console.SetOut(originalOutput);
            }

            return writer.ToString().Trim();
        }


        Console.WriteLine("C# PROJECT TEST");
        Console.WriteLine("==============================");


        // ==========================================================
        // ITEMS
        // ==========================================================

        Section("Items");

        HealingPotion healingPotion = new HealingPotion(45);

        Check(
            "Healing Potion name",
            healingPotion.Name == "Healing Potion"
        );

        Check(
            "Healing Potion heal amount",
            healingPotion.HealAmount == 45
        );


        // ==========================================================
        // WEAPONS
        // ==========================================================

        Section("Weapons");

        PlayerWeapon playerWeapon =
            new PlayerWeapon("Test Sword", 30);

        EnemyWeapon enemyWeapon =
            new EnemyWeapon("Test Claws", 20);

        VampireFangs vampireFangs =
            new VampireFangs("Test Fangs", 25, 1.75f);

        Check(
            "PlayerWeapon name",
            playerWeapon.Name == "Test Sword"
        );

        Check(
            "PlayerWeapon damage",
            playerWeapon.Damage == 30
        );

        Check(
            "EnemyWeapon damage",
            enemyWeapon.Damage == 20
        );

        Check(
            "VampireFangs damage",
            vampireFangs.Damage == 25
        );

        Check(
            "VampireFangs multiplier",
            vampireFangs.Multiplier == 1.75f
        );


        // ==========================================================
        // PLAYER CREATION
        // ==========================================================

        Section("Player Creation");

        int playerCountBefore = Player.PlayerCount;
        int aliveCountBefore = Player.PlayerAliveCount;

        Geralt geralt = new Geralt();

        Check(
            "Geralt name",
            geralt.Name == "Geralt"
        );

        Check(
            "Geralt starts with 250 health",
            geralt.Health == 250
        );

        Check(
            "Geralt starts alive",
            !geralt.IsDead
        );

        Check(
            "Geralt starts Idle",
            geralt.CurrentPlayerState == PlayerState.Idle
        );

        Check(
            "PlayerCount increased",
            Player.PlayerCount == playerCountBefore + 1
        );

        Check(
            "PlayerAliveCount increased",
            Player.PlayerAliveCount == aliveCountBefore + 1
        );

        Check(
            "Geralt has a Companion",
            geralt.Companion != null
        );

        Check(
            "Companion is Mina",
            geralt.Companion?.Name == "Mina"
        );


        // ==========================================================
        // PLAYER WEAPONS
        // ==========================================================

        Section("Player Weapons");

        string weapons = CaptureOutput(() =>
        {
            geralt.ShowWeapons();
        });

        string[] weaponNames = weapons.Split(
            Environment.NewLine,
            StringSplitOptions.RemoveEmptyEntries
        );

        Check(
            "Geralt has two weapons",
            weaponNames.Length == 2
        );

        Check(
            "Geralt has Silver Sword",
            weaponNames.Contains("Silver Sword")
        );

        Check(
            "Geralt has Steel Sword",
            weaponNames.Contains("Steel Sword")
        );


        CaptureOutput(() =>
        {
            geralt.AddWeapon(
                new PlayerWeapon("Silver Sword", 999)
            );
        });

        weapons = CaptureOutput(() =>
        {
            geralt.ShowWeapons();
        });

        weaponNames = weapons.Split(
            Environment.NewLine,
            StringSplitOptions.RemoveEmptyEntries
        );

        Check(
            "Duplicate weapon was rejected",
            weaponNames.Length == 2
        );


        // ==========================================================
        // SELF ATTACK
        // ==========================================================

        Section("Self Attack Protection");

        float healthBeforeSelfAttack = geralt.Health;

        geralt.Attack(geralt);

        Check(
            "Geralt cannot attack himself",
            geralt.Health == healthBeforeSelfAttack
        );


        // ==========================================================
        // BASIC COMBAT
        // ==========================================================

        Section("Basic Combat");

        Vampire vampire = new Vampire();

        float vampireHealthBefore = vampire.Health;

        geralt.Attack(vampire);

        float playerDamage =
            vampireHealthBefore - vampire.Health;

        Check(
            "Geralt damages Vampire",
            playerDamage == 10 ||
            playerDamage == 25
        );


        float geraltHealthBefore = geralt.Health;

        vampire.Attack(geralt);

        float vampireDamage =
            geraltHealthBefore - geralt.Health;

        Check(
            "Vampire ClawAttack deals 23 damage",
            vampireDamage == 23
        );


        // ==========================================================
        // ATTACK HISTORY
        // ==========================================================

        Section("Attack History");

        string attackHistory = CaptureOutput(() =>
        {
            Config.ShowAttackHistory();
        });

        Console.WriteLine($"History: {attackHistory}");

        Check(
            "History contains Player then Enemy",
            attackHistory == "Player Enemy"
        );


        // ==========================================================
        // PLAYER ATTACK STREAK
        // ==========================================================

        Section("Player Attack Streak");

        Geralt streakGeralt = new Geralt();
        Vampire streakVampire = new Vampire();

        streakGeralt.AddAttackToPlayerAttackList();
        streakGeralt.AddAttackToPlayerAttackList();

        Check(
            "Multiplier activates after two attacks",
            streakGeralt.IsPlayerMultiplierAllowed
        );

        float streakEnemyHealthBefore =
            streakVampire.Health;

        streakGeralt.Attack(streakVampire);

        float multipliedDamage =
            streakEnemyHealthBefore -
            streakVampire.Health;

        Check(
            "Player multiplier doubles weapon damage",
            multipliedDamage == 20 ||
            multipliedDamage == 50
        );

        streakVampire.Attack(streakGeralt);

        Check(
            "Enemy attack clears Player streak",
            !streakGeralt.IsPlayerMultiplierAllowed
        );


        // ==========================================================
        // ENEMY ATTACK STREAK
        // ==========================================================

        Section("Enemy Attack Streak");

        Vampire streakTestVampire = new Vampire();

        streakTestVampire.AddAttackToEnemyAttackList();
        streakTestVampire.AddAttackToEnemyAttackList();
        streakTestVampire.AddAttackToEnemyAttackList();

        Check(
            "Enemy multiplier activates after three attacks",
            streakTestVampire.IsEnemyMultiplierAllowed
        );

        streakTestVampire.ClearEnemyAttackList();

        Check(
            "Enemy attack list can be cleared",
            !streakTestVampire.IsEnemyMultiplierAllowed
        );


        // ==========================================================
        // COMPANION HEALING
        // ==========================================================

        Section("Companion Healing");

        Geralt healingGeralt = new Geralt();

        DamageResult healingResult =
            healingGeralt.TakeDamage(240);

        Check(
            "Geralt survives heavy damage",
            healingResult == DamageResult.Survived
        );

        Check(
            "Mina automatically heals wounded Geralt",
            healingGeralt.Health == 25
        );

        Check(
            "Geralt returns to Healthy state",
            healingGeralt.CurrentHealthState ==
            HealthState.Healthy
        );


        // ==========================================================
        // COMPANION TRANSFORMATION
        // ==========================================================

        Section("Companion Transformation");

        Helper mina = new Helper();

        DamageResult transformationResult =
            mina.TakeDamage(20);

        Check(
            "Mina survives transformation hit",
            transformationResult ==
            DamageResult.Survived
        );

        Check(
            "Mina transforms at 5 HP",
            mina.CurrentCompanionState ==
            CompanionState.TurnedIntoVampire
        );

        Check(
            "Transformation sets Mina health to 100",
            mina.Health == 100
        );


        // ==========================================================
        // VAMPIRE COMPANION HEALING
        // ==========================================================

        Section("Vampire Companion Healing");

        Geralt vampireHealGeralt = new Geralt();

        if (vampireHealGeralt.Companion != null)
        {
            vampireHealGeralt.Companion.TakeDamage(20);

            Check(
                "Geralt's Companion transformed",
                vampireHealGeralt.Companion.CurrentCompanionState ==
                CompanionState.TurnedIntoVampire
            );

            vampireHealGeralt.TakeDamage(240);

            Check(
                "Vampire Mina heals 50 HP",
                vampireHealGeralt.Health == 60
            );
        }
        else
        {
            Check(
                "Geralt has Companion for Vampire heal test",
                false
            );
        }


        // ==========================================================
        // COMPANION DEATH
        // ==========================================================

        Section("Companion Death");

        Helper deadMina = new Helper();

        DamageResult companionDeath =
            deadMina.TakeDamage(25);

        Check(
            "Lethal damage kills Mina",
            companionDeath == DamageResult.Killed
        );

        Check(
            "Dead Mina has zero health",
            deadMina.Health == 0
        );

        Check(
            "Dead Mina does not transform",
            deadMina.CurrentCompanionState !=
            CompanionState.TurnedIntoVampire
        );

        DamageResult damageDeadCompanion =
            deadMina.TakeDamage(10);

        Check(
            "Damage to dead Companion returns AlreadyDead",
            damageDeadCompanion ==
            DamageResult.AlreadyDead
        );


        // ==========================================================
        // PLAYER DEATH
        // ==========================================================

        Section("Player Death");

        Geralt deadGeralt = new Geralt();

        int aliveBeforeDeath =
            Player.PlayerAliveCount;

        DamageResult playerDeath =
            deadGeralt.TakeDamage(999);

        Check(
            "Lethal damage returns Killed",
            playerDeath == DamageResult.Killed
        );

        Check(
            "Geralt health cannot fall below zero",
            deadGeralt.Health == 0
        );

        Check(
            "Geralt IsDead becomes true",
            deadGeralt.IsDead
        );

        Check(
            "PlayerAliveCount decreases",
            Player.PlayerAliveCount ==
            aliveBeforeDeath - 1
        );

        DamageResult deadPlayerDamage =
            deadGeralt.TakeDamage(10);

        Check(
            "Damage to dead Player returns AlreadyDead",
            deadPlayerDamage ==
            DamageResult.AlreadyDead
        );


        // ==========================================================
        // ENEMY DEATH
        // ==========================================================

        Section("Enemy Death");

        Vampire deadVampire = new Vampire();

        DamageResult vampireDeath =
            deadVampire.TakeDamage(999);

        Check(
            "Lethal damage kills Vampire",
            vampireDeath == DamageResult.Killed
        );

        Check(
            "Vampire health cannot fall below zero",
            deadVampire.Health == 0
        );

        Check(
            "Vampire IsDead becomes true",
            deadVampire.IsDead
        );

        DamageResult deadVampireDamage =
            deadVampire.TakeDamage(10);

        Check(
            "Damage to dead Vampire returns AlreadyDead",
            deadVampireDamage ==
            DamageResult.AlreadyDead
        );


        // ==========================================================
        // STATES
        // ==========================================================

        Section("States");

        Geralt stateGeralt = new Geralt();

        stateGeralt.ChangePlayerState(
            PlayerState.Attacking
        );

        Check(
            "PlayerState can change",
            stateGeralt.CurrentPlayerState ==
            PlayerState.Attacking
        );

        stateGeralt.ChangeHealthState(
            HealthState.Critical
        );

        Check(
            "HealthState can change",
            stateGeralt.CurrentHealthState ==
            HealthState.Critical
        );


        // ==========================================================
        // RESULT
        // ==========================================================

        Console.WriteLine();
        Console.WriteLine("==============================");
        Console.WriteLine("TEST RESULT");
        Console.WriteLine("==============================");

        Console.WriteLine($"Passed: {passed}");
        Console.WriteLine($"Failed: {failed}");

        Console.WriteLine();

        if (failed == 0)
            Console.WriteLine("ALL TESTS PASSED");
        else
            Console.WriteLine("SOME TESTS FAILED");
    }
}