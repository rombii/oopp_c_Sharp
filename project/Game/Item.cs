using System;

namespace project.Game;

public class Item : Entity
{
    private static int _counter;
    private readonly int _itemId = _counter++;

    public enum EType
    {
        Heal, Weapon
    }
    public EType Type { get; }
    public string? Name { get; }
    public Element? Element { get; }
    public int DmgMin { get; }
    public int DmgMax { get; }
    
    //Konstruktor dla itemów leczących
    public Item(string name, Uri sprite, int healAmount) : base(sprite)
    {
        Name = name;
        DmgMin = DmgMax = healAmount;
        Type = EType.Heal;
    }
    //Konstruktor dla broni
    public Item(string name, Uri sprite, int dmgMin, int dmgMax, Element? element = null) : base(sprite)
    {
        Name = name;
        DmgMin = dmgMin;
        DmgMax = dmgMax;
        Type = EType.Weapon;
        Element = element;
    }

    public Item(Models.Item model) : base(model.SpriteUrl)
    {
        Name = model.Name;
        DmgMin = model.DmgMin;
        DmgMax = model.DmgMax;
        Type = model.Type;
        Element = model.Element;
    }
    public int GetDmg()
    {
        var random = new Random();
        return random.Next(DmgMin, DmgMax + 1);
    }
}