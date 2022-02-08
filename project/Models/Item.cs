using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using project.Game;

namespace project.Models;

public class Item
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ItemId { get; set; }
    public string? Name { get; set; }
    public Uri? SpriteUrl { get; set; }
    public virtual Element? Element { get; set; }
    public int DmgMin { get; set; }
    public int DmgMax { get; set; }
    public Game.Item.EType Type { get; set; }

    public Item()
    {
    }

    public Item(string name, Uri spriteUrl, int dmgMin, int dmgMax, Element element)
    {
        Name = name;
        SpriteUrl = spriteUrl;
        DmgMin = dmgMin;
        DmgMax = dmgMax;
        Element = element;
        Type = Game.Item.EType.Weapon;
    }

    public Item(string name, Uri spriteUrl, int healAmount)
    {
        Name = name;
        SpriteUrl = spriteUrl;
        DmgMin = DmgMax = healAmount;
        Type = Game.Item.EType.Heal;
    }
}