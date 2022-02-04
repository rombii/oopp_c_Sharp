using System;

namespace project.Game;

public class Enemy : Mob
{
    private static int _counter = 0;
    private readonly int _enemyId = _counter++;
    public string? Name { get; }
    public Item? Carrying { get; private set; }
    public Element? Element { get; }
    public int DmgMin { get; }
    public int DmgMax { get; }

    public Enemy(string name, string sprite, int hp, int dmgMin, int dmgMax, int x, int y) : base(sprite, hp, x, y)
    {
        Name = name;
        DmgMin = dmgMin;
        DmgMax = dmgMax;
    }

    public Enemy(string name, string sprite, int hp, int dmgMin, int dmgMax, int x, int y, Element element) : this(name,
        sprite, hp, dmgMin, dmgMax, x, y)
    {
        Element = element;
    }

    public Enemy(string name, string sprite, int hp, int dmgMin, int dmgMax, int x, int y, Item carrying) : this(name,
        sprite, hp, dmgMin, dmgMax, x, y)
    {
        Carrying = carrying;
    }

    public Enemy(string name, string sprite, int hp, int dmgMin, int dmgMax, int x, int y, Element element,
        Item carrying) : this(name, sprite, hp, dmgMin, dmgMax, x, y)
    {
        Element = element;
        Carrying = carrying;
    }

    public Enemy(Models.Enemy model, int x, int y) : base(model.Sprite, model.Health, x, y)
    {
        Name = model.Name;
        DmgMin = model.DmgMin;
        DmgMax = model.DmgMax;
        if (model.Item != null)
            Carrying = new Item(model.Item);
        Element = model.Element;
    }

    public int GetDmg()
    {
        var random = new Random();
        return random.Next(DmgMin, DmgMax + 1);
    }

    public void TakeDmg(int dmg, Element? attackingElement)
    {
        if (Element != null && attackingElement != null)
        {
            if (Element.WeakToId == attackingElement.ElementId)
            {
                //LogBox
                TakeDmg(dmg*2);
                return;
            }

            if (Element.StrongToId == attackingElement.ElementId)
            {
                //LogBox
                TakeDmg(dmg/2);
                return;
            }
        }
        TakeDmg(dmg);
    }
    //LogBox TakeDmg override
    public override void Pickup(Item item)
    {
        if (Carrying == null) Carrying = item;
        //TODO else Drop(item);
    }

    public override void Interact(int x, int y)
    {
        //TODO
    }
}