using System;
using System.Windows.Media.Imaging;

namespace project.Game;

public class Element
{
    public int ElementId { get; private set; }
    public string? Name { get; set; }
    public string? SpriteUrl { get; set; }
    public int WeakToId { get; set; } = -1;
    public int StrongToId { get; set; } = -1;

    public BitmapImage Sprite
    {
        get
        {
            var sprite = new BitmapImage();
            sprite.BeginInit();
            sprite.UriSource = new Uri(SpriteUrl);
            sprite.DecodePixelWidth = 32;
            sprite.EndInit();
            return sprite;
        }
    }
}