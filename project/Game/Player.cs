using System.Buffers.Text;
using System.Linq;

namespace project.Game;

public class Player : Mob
{
    private readonly Item?[] _inventory = new Item[10];
    public int EquippedItemId { get; set; } = 0;
    public Item? EquippedItem => _inventory[EquippedItemId];

    public Player(string sprite, int startHp) : base(sprite, startHp, 4, 4)
    {
    }

    public bool CheckForWeapon(Models.Item weapon)
    {
        return weapon.Type != Item.EType.Heal && _inventory.Where(item => item != null).Any(item => item.Name == weapon.Name && item.Element == weapon.Element && item.DmgMin == weapon.DmgMin && item.DmgMax == weapon.DmgMax);
    }

    public void UseItem()
    {
        if (EquippedItem is not {Type: Item.EType.Heal}) return;
        Heal(EquippedItem.DmgMin);
        _inventory[EquippedItemId] = null;
    }

    public void DropCurrentItem()
    {
        //TODO Drop(_inventory[EquippedItemId]);
        _inventory[EquippedItemId] = null;
    }

    public void DeleteCurrentItem()
    {
        _inventory[EquippedItemId] = null;
    }

    //TODO LogBox Heal & TakeDmg overrides
    public override void Pickup(Item item)
    {
        for (var i = 0; i <= _inventory.Length; i++)
        {
            if (i == _inventory.Length)
            {
                //TODO Drop(item);
            }
            else
            {
                if (_inventory[i] != null) continue;
                _inventory[i] = item;
                //TODO Logbox
                break;
            }
        }
    }

    public override void Interact(int x, int y)
    {
        //TODO
    }
}