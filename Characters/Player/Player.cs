abstract class Player : Character
{
    public Companion? Companion { get; protected set; }
    private List<int> _playerAttackCountList = new List<int>();
    protected List<PlayerWeapon> _playerWeapons = new List<PlayerWeapon>();
    protected List<Items> _playerItems = new List<Items>();

    public void AddItem(Items item)
    {
        foreach (Items existingItem in _playerItems)
        {
            if (existingItem.Name == item.Name)
            {
                Console.WriteLine("already exists");
                return;
            }
        }

        _playerItems.Add(item);
    }

    public void ShowItems()
    {
        foreach (Items item in _playerItems)
        {
            Console.WriteLine($"{item.Name}");
        }
    }

    public void AddWeapon(PlayerWeapon weapon)
    {
        foreach(Weapon existingWeapon in _playerWeapons)
        {
            if (existingWeapon.Name == weapon.Name)
            {
                Console.WriteLine("already exists");
                return;
            }
        }

        _playerWeapons.Add(weapon);
    }

    public void ShowWeapons()
    {
        foreach (Weapon weapon in _playerWeapons)
        {
            Console.WriteLine($"{weapon.Name}");
        }
    }

    public void AddAttackToPlayerAttackList()
    {
        _playerAttackCountList.Add(1);
    } 

    public void ClearPlayerAttackList()
    {
        _playerAttackCountList.Clear();
    }

    public void ShowAttackList()
    {
        foreach (int attack in _playerAttackCountList)
        {
            Console.Write($"{attack} ");
        }
    }

    public Player(string name, float health)
        : base(name, health)
    {   
        PlayerCount++;

        if (!IsDead)
        {
            PlayerAliveCount++;
            ChangePlayerState(PlayerState.Idle);
        }
    }

    #region Player Properties
    private PlayerState _currentPlayerState;
    public PlayerState CurrentPlayerState
    {
        get
        {
            return _currentPlayerState;
        }

        private set
        {
            _currentPlayerState = value;
        }
    }

    private HealthState _currentHealthState;
    public HealthState CurrentHealthState
    {
        get
        {
            return _currentHealthState;
        }

        private set
        {
            _currentHealthState = value;
        }
    }

    private static int _playerCount = 0;
    public static int PlayerCount
    {
        get
        {
            return _playerCount;
        }

        private set
        {
            _playerCount = value;
        }
    }

    private static int _playerAliveCount = 0;
    public static int PlayerAliveCount
    {
        get
        {
            return _playerAliveCount;
        }

        protected set
        {
            _playerAliveCount = value;
        }
    }

    public virtual bool IsWounded => Health <= 20 && Health > 0;
    public virtual bool IsPlayerMultiplierAllowed => _playerAttackCountList.Count >= 2;
    public bool CanCompanionHeal => Companion != null
                                && !Companion.IsDead
                                    && Companion.Heal > 0
                                        && IsWounded;
    #endregion

    public virtual void Attack(IDamageable target)
    {
        // is attacker dead?
        if (this.IsDead)
            return;

        // self cannot be attacked
        if (target == this)
            return;

        PerformAttack(target);
    }
    
    protected abstract void PerformAttack(IDamageable target);

    public override DamageResult TakeDamage(float amount)
    {
        // is dead before damage?
        if (IsDead)
            return DamageResult.AlreadyDead;

        Health -= amount;

        while (CanCompanionHeal && Companion != null)
        {
            ChangeHealthState(HealthState.Wounded);
            Console.WriteLine($"HealthState: {CurrentHealthState}");

            Companion.HealPlayer(this);
        }

        if (!IsDead && !IsWounded)
            ChangeHealthState(HealthState.Healthy);
        
        // did damage kill?
        if (IsDead)
        {
            PlayerAliveCount--;
            return DamageResult.Killed;
        }

        // did survive
        return DamageResult.Survived;
    }

    public void AddHealth(float heal)
    {
        Health += heal;
    }

    public void ChangePlayerState(PlayerState newState)
    {
        if (newState == CurrentPlayerState)
            return;

        CurrentPlayerState = newState;
    }

    public void ChangeHealthState(HealthState newState)
    {
        if (newState == CurrentHealthState)
            return;

        CurrentHealthState = newState;
    }
}