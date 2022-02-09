using System;

namespace project.Game;

public class Enemy : Mob
{
    private static int _counter;
    public int Id { get; } = _counter++;
    public string? Name { get; }
    public Item? Carrying { get; private set; }
    public Element? Element { get; }
    public int DmgMin { get; }
    public int DmgMax { get; }

    public Enemy(string name, Uri sprite, int hp, int dmgMin, int dmgMax, int x, int y, GameWindow game, Element? element = null,  Item? item = null) : base(sprite, hp, x, y, game)
    {
        Name = name;
        DmgMin = dmgMin;
        DmgMax = dmgMax;
        Element = element;
        Carrying = item;
    }

    public Enemy(Models.Enemy model, int x, int y, GameWindow game) : base(model.Sprite ?? throw new InvalidOperationException(), model.Health, x, y, game)
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
                Game.LogBlock.AddLine("Skuteczne trafienie");
                TakeDmg(dmg*2);
                return;
            }

            if (Element.StrongToId == attackingElement.ElementId)
            {
                Game.LogBlock.AddLine("Nieskuteczne trafienie");
                TakeDmg(dmg/2);
                return;
            }
        }
        TakeDmg(dmg);
    }

    public override void TakeDmg(int dmg)
    {
        base.TakeDmg(dmg);
        Game.LogBlock.AddLine(Name + " został zaatakowany za " + dmg + " obrażeń");
    }
    public override void Pickup(Item item)
    {
        if (Carrying == null) Carrying = item;
        else Drop(item);
    }

    public override void Interact(int x, int y)
    {
        if (Game.Player == null) return;
        if (x == Game.Player.X && y == Game.Player.Y)
        {
            Game.Player.TakeDmg(GetDmg());
            if (Game.Player.Hp == 0)
            {
                Game.Player = null;
                Game.EntityTable[x, y] = new Entity();
                Game.SetGameOver();
            }
        }
        else if (Game.EntityTable[x, y] is Item)
        {
            var temp = (Item) Game.EntityTable[x, y];
            Teleport(x, y);
            if (Carrying != null)
                Drop(temp);
            else
                Carrying = temp;
        }
    }
}