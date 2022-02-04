namespace project.Game;

public abstract class Mob : Entity
{
    public int Hp { get; private set; }
    public int X { get; }
    public int Y { get; }
    private int _maxHp;

    public void TakeDmg(int dmg)
    {
        if (Hp - dmg < 0) Hp = 0;
        else Hp -= dmg;
    }

    public void Heal(int heal)
    {
        if (Hp + heal > _maxHp) Hp = _maxHp;
        else Hp += heal;
    }

    public abstract void Interact(int x, int y);
    public abstract void Pickup(Item item);
    //TODO public void Drop(Item item)
    //TODO public void Teleport(int x, int y)
    //TODO public void MoveUpDownLeftRightidiagonaleteż()

    public Mob(string sprite, int startHp, int x, int y) : base(sprite)
    {
        Hp = _maxHp = startHp;
        X = x;
        Y = y;
        //TODO podstawienie do głównej tablicy
    }
}