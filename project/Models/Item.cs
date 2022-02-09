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
}