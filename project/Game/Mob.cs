using System;

namespace project.Game;

public abstract class Mob : Entity
{
    public int Hp { get; private set; }
    public int X { get; private set; }
    public int Y { get; private set; }
    private int _maxHp;
    private protected GameWindow _game;

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
        if(Y!=0 && _game.EntityTable[X, Y-1].Empty) _game.EntityTable[X, Y-1] = item;                         //N
        else if(X!=8 && _game.EntityTable[X+1, Y].Empty) _game.EntityTable[X+1, Y] = item;                    //E
        else if(Y!=8 && _game.EntityTable[X, Y+1].Empty) _game.EntityTable[X, Y+1] = item;                    //S
        else if(X!=0 && _game.EntityTable[X-1, Y].Empty) _game.EntityTable[X-1, Y] = item;                    //W
        else if((X!=8 && Y!=0) && _game.EntityTable[X+1, Y-1].Empty) _game.EntityTable[X+1, Y-1] = item;      //NE
        else if((X!=8 && Y!=8) && _game.EntityTable[X+1, Y+1].Empty) _game.EntityTable[X+1, Y+1] = item;      //SE
        else if((X!=0 && Y!=8) && _game.EntityTable[X-1, Y+1].Empty) _game.EntityTable[X-1, Y+1] = item;      //SW
        else if((X!=0 && Y!=0) && _game.EntityTable[X-1, Y-1].Empty) _game.EntityTable[X-1, Y-1] = item;      //NW
    }

    public void Teleport(int x, int y)
    {
        _game.EntityTable[X, Y] = new Entity();
        _game.EntityTable[x, y] = this;
        X = x;
        Y = y;
    }

    public void MoveUp()
    {
        if (Y == 0) return;
        if (_game.EntityTable[X, Y - 1].Empty)
        {
            _game.EntityTable[X, Y] = new Entity();
            _game.EntityTable[X, --Y] = this;
        }
        else
        {
            Interact(X, Y - 1);
        }
    }

    public void MoveDown()
    {
        if (Y == 8) return;
        if (_game.EntityTable[X, Y + 1].Empty)
        {
            _game.EntityTable[X, Y] = new Entity();
            _game.EntityTable[X, ++Y] = this;
        }
        else
        {
            Interact(X, Y + 1);
        }
    }
    public void MoveLeft()
    {
        if (X == 0) return;
        if (_game.EntityTable[X - 1, Y].Empty)
        {
            _game.EntityTable[X, Y] = new Entity();
            _game.EntityTable[--X, Y] = this;
        }
        else
        {
            Interact(X - 1, Y);
        }
    }
    public void MoveRight()
    {
        if (X == 8) return;
        if (_game.EntityTable[X + 1, Y].Empty)
        {
            _game.EntityTable[X, Y] = new Entity();
            _game.EntityTable[++X, Y] = this;
        }
        else
        {
            Interact(X + 1, Y);
        }
    }
    public void MoveUpLeft()
    {
        if (X == 0 || Y == 0) return;
        if (_game.EntityTable[X - 1, Y - 1].Empty)
        {
            _game.EntityTable[X, Y] = new Entity();
            _game.EntityTable[--X, --Y] = this;
        }
        else
        {
            Interact(X - 1, Y - 1);
        }
    }
    public void MoveUpRight()
    {
        if (X == 8 || Y == 0) return;
        if (_game.EntityTable[X + 1, Y - 1].Empty)
        {
            _game.EntityTable[X, Y] = new Entity();
            _game.EntityTable[++X, --Y] = this;
        }
        else
        {
            Interact(X + 1, Y - 1);
        }
    }
    public void MoveDownLeft()
    {
        if (X == 0 || Y == 8) return;
        if (_game.EntityTable[X - 1, Y + 1].Empty)
        {
            _game.EntityTable[X, Y] = new Entity();
            _game.EntityTable[--X, ++Y] = this;
        }
        else
        {
            Interact(X - 1, Y + 1);
        }
    }
    public void MoveDownRight()
    {
        if (X == 8 || Y == 8) return;
        if (_game.EntityTable[X + 1, Y + 1].Empty)
        {
            _game.EntityTable[X, Y] = new Entity();
            _game.EntityTable[++X, ++Y] = this;
        }
        else
        {
            Interact(X + 1, Y + 1);
        }
    }

    protected Mob(Uri sprite, int startHp, int x, int y, GameWindow game) : base(sprite)
    {
        _game = game;
        Hp = _maxHp = startHp;
        X = x;
        Y = y;
        _game.EntityTable[x, y] = this;
    }
}