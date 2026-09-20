static class Config
{
    private static List<AttackerType> _attackHistory = new List<AttackerType>();

    public static void AddAttackToAttackHistory(AttackerType attacker)
    {
        _attackHistory.Add(attacker);
    }

    public static void ShowAttackHistory()
    {
        foreach (AttackerType attack in _attackHistory)
            Console.Write($"{attack} ");
    }
}