using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.Log("warning more than inventory instances found");
            return;
        }
        instance = this;
    }

    public delegate void OnItemChanged();
    public OnItemChanged onItemAddedCallback;

    public int space = 20;

    public List<Item> items = new List<Item>();

    public void Add(Item item)
    {
        if (items.Count >= space && !item.baseItem.isStackable)
        {
            Debug.Log("Not enough room.");
            return;
        }
        else if (items.Contains(item) && item.baseItem.isStackable)
        {
            item.AddStackCount(item.baseItem.stackCount);
        }
        else
        {
            items.Add(item);
        }

        if (onItemAddedCallback != null)
            onItemAddedCallback.Invoke();
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemAddedCallback != null)
            onItemAddedCallback.Invoke();
    }

}