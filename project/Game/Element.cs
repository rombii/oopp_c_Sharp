using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Media.Imaging;

namespace project.Game;

public class Element
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ElementId { get; set; }
    public string? Name { get; set; }
    public string? SpriteUrl { get; set; }
    public int WeakToId { get; set; } = -1;
    public int StrongToId { get; set; } = -1;

    public BitmapImage? Sprite
    {
        get
        {
            if (SpriteUrl == null) return null;
            var sprite = new BitmapImage();
            sprite.BeginInit();
            sprite.UriSource = new Uri(SpriteUrl);
            sprite.DecodePixelWidth = 32;
            sprite.EndInit();
            return sprite;
        }
    }
}