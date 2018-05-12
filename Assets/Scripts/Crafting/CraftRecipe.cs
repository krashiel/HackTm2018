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
    // Use this for initialization
    void Start()
    {
        inventoryItems = Inventory.instance.items;
    }

    public void checkIfEligible()
    {
        inventoryItems = Inventory.instance.items;

        var isEligible = false;
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
}
