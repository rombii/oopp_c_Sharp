using System;
using System.Linq;

namespace project.Game;

public class Player : Mob
{
    public Item?[] Inventory { get; } = new Item[10];
    public int EquippedItemId { get; set; }
    public Item? EquippedItem
    {
        get => Inventory[EquippedItemId];
        set => Inventory[EquippedItemId] = value;
    }

    public Player(Uri sprite, int startHp, GameWindow game) : base(sprite, startHp, 4, 4, game)
    {
    }

    public bool CheckForWeapon(Models.Item weapon)
    {
        return weapon.Type != Item.EType.Heal && Inventory.Where(item => item != null).Any(item => item.Name == weapon.Name && item.Element == weapon.Element && item.DmgMin == weapon.DmgMin && item.DmgMax == weapon.DmgMax);
    }

    public void UseItem()
    {
        if (EquippedItem is not {Type: Item.EType.Heal}) return;
        Heal(EquippedItem.DmgMin);
        EquippedItem = null;
    }

    public void DropCurrentItem()
    {
        if (EquippedItem == null) return;
        Drop(EquippedItem);
        EquippedItem = null;
    }

    public void DeleteCurrentItem()
    {
        Inventory[EquippedItemId] = null;
    }

    public override void Heal(int heal)
    {
        base.Heal(heal);
        Game.LogBlock.AddLine("Uleczono " + heal + " punktów zdrowia");
    }

    public override void TakeDmg(int dmg)
    {
        base.TakeDmg(dmg);
        Game.LogBlock.AddLine("Otrzymano " + dmg + " obrażeń");
    }

    public override void Pickup(Item item)
    {
        for (var i = 0; i <= Inventory.Length; i++)
        {
            if (i == Inventory.Length)
            {
                Drop(item);
            }
            else
            {
                if (Inventory[i] != null) continue;
                Inventory[i] = item;
                //TODO Logbox
                break;
            }
        }
    }

    public override void Interact(int x, int y)
    {
        switch (Game.EntityTable[x, y])
        {
            case Enemy when EquippedItem == null:
                return;
            case Enemy:
            {
                if (EquippedItem.Type == Item.EType.Weapon)
                {
                    ((Enemy) Game.EntityTable[x, y]).TakeDmg(EquippedItem.GetDmg(), EquippedItem.Element);
                    if (((Enemy) Game.EntityTable[x, y]).Hp == 0)
                    {
                        Game.LogBlock.AddLine("Pokonano "+((Enemy) Game.EntityTable[x, y]).Name);
                        if (((Enemy) Game.EntityTable[x, y]).Carrying == null)
                            Game.EntityTable[x, y] = new Entity();
                        else
                            Game.EntityTable[x, y] = ((Enemy) Game.EntityTable[x, y]).Carrying;
                    }
                }

                break;
            }
            case Item:
            {
                var temp = (Item) Game.EntityTable[x, y];
                Teleport(x, y);
                Pickup(temp);
                break;
            }
        }
    }
}