using System;
using System.Windows.Media.Imaging;

namespace project.Game;

public class Entity
{
    public BitmapImage Sprite { get; }
    public bool Empty { get; }

    protected Entity(Uri spriteUri)
    {
        Sprite = new BitmapImage();
        Sprite.BeginInit();
            Sprite.UriSource = spriteUri;
            Sprite.DecodePixelWidth = 32;
        Sprite.EndInit();
        Empty = false;
    }

    public Entity() : this(new Uri(Environment.CurrentDirectory.Remove(Environment.CurrentDirectory.Length - 25) + "/" + "res/img/emptyO.png", UriKind.RelativeOrAbsolute)) //Puste pole
    {
        Empty = true;
    }
}