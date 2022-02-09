using System;

namespace project.Game;

public abstract class Mob : Entity
{
    public int Hp { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }
    private readonly int _maxHp;
    private protected readonly GameWindow Game;

    public virtual void TakeDmg(int dmg)
    {
        if (Hp - dmg < 0) Hp = 0;
        else Hp -= dmg;
    }

    public virtual void Heal(int heal)
    {
        if (Hp + heal > _maxHp) Hp = _maxHp;
        else Hp += heal;
    }

    public abstract void Interact(int x, int y);
    public abstract void Pickup(Item item);

    public void Drop(Item item)
    {
        if(Y!=0 && Game.EntityTable[X, Y-1].Empty) Game.EntityTable[X, Y-1] = item;                         //N
        else if(X!=8 && Game.EntityTable[X+1, Y].Empty) Game.EntityTable[X+1, Y] = item;                    //E
        else if(Y!=8 && Game.EntityTable[X, Y+1].Empty) Game.EntityTable[X, Y+1] = item;                    //S
        else if(X!=0 && Game.EntityTable[X-1, Y].Empty) Game.EntityTable[X-1, Y] = item;                    //W
        else if((X!=8 && Y!=0) && Game.EntityTable[X+1, Y-1].Empty) Game.EntityTable[X+1, Y-1] = item;      //NE
        else if((X!=8 && Y!=8) && Game.EntityTable[X+1, Y+1].Empty) Game.EntityTable[X+1, Y+1] = item;      //SE
        else if((X!=0 && Y!=8) && Game.EntityTable[X-1, Y+1].Empty) Game.EntityTable[X-1, Y+1] = item;      //SW
        else if((X!=0 && Y!=0) && Game.EntityTable[X-1, Y-1].Empty) Game.EntityTable[X-1, Y-1] = item;      //NW
    }

    public void Teleport(int x, int y)
    {
        Game.EntityTable[X, Y] = new Entity();
        Game.EntityTable[x, y] = this;
        X = x;
        Y = y;
    }

    public void MoveUp()
    {
        if (Y == 0) return;
        if (Game.EntityTable[X, Y - 1].Empty)
        {
            Game.EntityTable[X, Y] = new Entity();
            Game.EntityTable[X, --Y] = this;
        }
        else
        {
            Interact(X, Y - 1);
        }
    }

    public void MoveDown()
    {
        if (Y == 8) return;
        if (Game.EntityTable[X, Y + 1].Empty)
        {
            Game.EntityTable[X, Y] = new Entity();
            Game.EntityTable[X, ++Y] = this;
        }
        else
        {
            Interact(X, Y + 1);
        }
    }
    public void MoveLeft()
    {
        if (X == 0) return;
        if (Game.EntityTable[X - 1, Y].Empty)
        {
            Game.EntityTable[X, Y] = new Entity();
            Game.EntityTable[--X, Y] = this;
        }
        else
        {
            Interact(X - 1, Y);
        }
    }
    public void MoveRight()
    {
        if (X == 8) return;
        if (Game.EntityTable[X + 1, Y].Empty)
        {
            Game.EntityTable[X, Y] = new Entity();
            Game.EntityTable[++X, Y] = this;
        }
        else
        {
            Interact(X + 1, Y);
        }
    }
    public void MoveUpLeft()
    {
        if (X == 0 || Y == 0) return;
        if (Game.EntityTable[X - 1, Y - 1].Empty)
        {
            Game.EntityTable[X, Y] = new Entity();
            Game.EntityTable[--X, --Y] = this;
        }
        else
        {
            Interact(X - 1, Y - 1);
        }
    }
    public void MoveUpRight()
    {
        if (X == 8 || Y == 0) return;
        if (Game.EntityTable[X + 1, Y - 1].Empty)
        {
            Game.EntityTable[X, Y] = new Entity();
            Game.EntityTable[++X, --Y] = this;
        }
        else
        {
            Interact(X + 1, Y - 1);
        }
    }
    public void MoveDownLeft()
    {
        if (X == 0 || Y == 8) return;
        if (Game.EntityTable[X - 1, Y + 1].Empty)
        {
            Game.EntityTable[X, Y] = new Entity();
            Game.EntityTable[--X, ++Y] = this;
        }
        else
        {
            Interact(X - 1, Y + 1);
        }
    }
    public void MoveDownRight()
    {
        if (X == 8 || Y == 8) return;
        if (Game.EntityTable[X + 1, Y + 1].Empty)
        {
            Game.EntityTable[X, Y] = new Entity();
            Game.EntityTable[++X, ++Y] = this;
        }
        else
        {
            Interact(X + 1, Y + 1);
        }
    }

    protected Mob(Uri sprite, int startHp, int x, int y, GameWindow game) : base(sprite)
    {
        Game = game;
        Hp = _maxHp = startHp;
        X = x;
        Y = y;
        Game.EntityTable[x, y] = this;
    }
}