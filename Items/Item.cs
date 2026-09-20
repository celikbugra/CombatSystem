abstract class Item
{
    public Item(string name)
    {
        Name = name;
    }

    public string Name { get; protected set; }
}

class HealingPotion : Item
{
    public float HealAmount { get; protected set; }

    public HealingPotion(float healAmount)
        : base("Healing Potion")
    {
        HealAmount = healAmount;
    }
}

class ValeBloodPotion : Item
{
    public float Toxicity { get; protected set; }
    public ValeBloodPotion(float toxicity)
        : base("Vale Blood")
    {
        Toxicity = toxicity;
    }
}