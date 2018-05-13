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
        this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Upgrade " + itemName;
    }

    public void checkIfEligible(bool shouldUpdateValues)
    {
        inventoryItems = Inventory.instance.items;

        for (var i = 0; i < necessaryItems.Count; i++)
        {
            gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.grey;

            var item = necessaryItems[i];
            if (inventoryItems.Contains(item))
            {
                var invItem = inventoryItems.FirstOrDefault(it => it.baseItem.id == item.baseItem.id);
                if (invItem.ItemStackCount() >= necessaryItemsNumber[i])
                {
                    gameObject.GetComponentInChildren<TextMeshProUGUI>().color = Color.cyan;
                    if (shouldUpdateValues)
                    {
                        invItem.AddStackCount(-necessaryItemsNumber[i]);
                    }
                    isEligible = true;
                }
            }
        }
    }

    public void CraftItem()
    {
        if (!isEligible)
        {
            Debug.Log("Not enough materials to craft this " + itemName);
            return;
        }
        checkIfEligible(true);
        character_movement.damagePower += 1;
        InventoryUI.Instance.RefreshUI();
        CraftRecipes.Instance.InitialiseCraftRecipesView();
        Debug.Log("Successfully upgraded weapon's power " + itemName);
        isEligible = false;
    }
}
