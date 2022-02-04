using project.Game;

namespace project.Models;

public class Enemy
{
    public int EnemyId { get; set; }
    public string? Name { get; set; }
    public string? Sprite { get; set; }
    public Element? Element { get; set; }
    public int Health { get; set; }
    public int DmgMin { get; set; }
    public int DmgMax { get; set; }
    public Item? Item { get; set; }

    public Enemy()
    {
    }

    public Enemy(string? name, string? sprite, int health, int dmgMin, int dmgMax, Item item, Element element)
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