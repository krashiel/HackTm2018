using UnityEngine;

public class Item : MonoBehaviour
{
    public BaseItem baseItem;
    private int itemStackCount = 1;

    public int ItemStackCount()
    {
        return itemStackCount;
    }

    public void AddStackCount(int addValue)
    {
        itemStackCount += addValue;
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
