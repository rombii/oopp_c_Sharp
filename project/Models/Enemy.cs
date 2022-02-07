using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using project.Game;

namespace project.Models;

public class Enemy
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EnemyId { get; set; }
    public string? Name { get; set; }
    public Uri? Sprite { get; set; }
    
    public virtual Element? Element { get; set; }
    public int Health { get; set; }
    public int DmgMin { get; set; }
    public int DmgMax { get; set; }
    public virtual Item? Item { get; set; }

    public Enemy()
    {
    }

    public Enemy(string? name, Uri? sprite, int health, int dmgMin, int dmgMax, Item item, Element element)
    {
        Name = name;
        Sprite = sprite;
        Element = element;
        Health = health;
        DmgMin = dmgMin;
        DmgMax = dmgMax;
        Item = item;
    }
}