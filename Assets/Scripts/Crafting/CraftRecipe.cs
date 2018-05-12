using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class CraftRecipe : MonoBehaviour
{

    public string itemName;
    public int itemId;
    public List<Item> necessaryItems;
    public List<int> necessaryItemsNumber;

    private List<Item> inventoryItems;
    private bool isEligible = false;
    // Use this for initialization
    void Start()
    {
        inventoryItems = Inventory.instance.items;
        this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Craft " + itemName;
    }

    public void checkIfEligible()
    {
        inventoryItems = Inventory.instance.items;

        for (var i = 0; i < necessaryItems.Count; i++)
        {
            var item = necessaryItems[i];
            if (inventoryItems.Contains(item))
            {
                var invItem = inventoryItems.FirstOrDefault(it => it.baseItem.id == item.baseItem.id);
                if (invItem.ItemStackCount() >= necessaryItemsNumber[i])
                {
                    isEligible = true;
                }
                else
                {
                    this.GetComponentInChildren<TextMeshProUGUI>().color = Color.grey;
                }
            }
        }
    }

    public void CraftItem()
    {
        checkIfEligible();
        if (isEligible)
        {
            Debug.Log("Crafting item with id {itemId}");
        }
        Debug.Log("Not enough materials to craft this item");
    }
}
