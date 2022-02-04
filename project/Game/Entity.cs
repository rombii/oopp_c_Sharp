using System;
using System.Windows.Media.Imaging;

namespace project.Game;

public class Entity
{
    public BitmapImage Sprite { get; }
    public bool Empty { get; }

    public Entity(string spriteUrl)
    {
        Sprite = new BitmapImage();
        Sprite.BeginInit();
            Sprite.UriSource = new Uri(spriteUrl);
            Sprite.DecodePixelWidth = 32;
        Sprite.EndInit();
        Empty = false;
    }

    public Entity() : this("res/img/empty.png") //Puste pole
    {
        Empty = true;
    }
}