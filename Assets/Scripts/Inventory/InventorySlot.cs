using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{

    public Image icon;
    public TMP_Text stackCount;
    public GameObject stackObject;
    public Button removeButton;

    public Item item;  // Current item in the slot

    // Add item to the slot
    public void AddItem(Item newItem)
    {
        if (item != null)
        {
            UpdateItemStackText(newItem);
        }
        item = newItem;
        icon.sprite = item.baseItem.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void UpdateItemStackText(Item newItem)
    {
        //if item is stackable update the stack count text
        if (newItem.baseItem.isStackable && item.baseItem.id == newItem.baseItem.id)
        {
            stackCount.text = item.ItemStackCount().ToString();
            stackObject.SetActive(true);
        }
    }

    // Clear the slot
    public void ClearSlot()
    {
        item = null;
        stackObject.SetActive(false);
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    // If the remove button is pressed, this function will be called.
    public void RemoveItemFromInventory()
    {
        if (item.ItemStackCount() > 1)
        {
            item.AddStackCount(-1);
            stackCount.text = item.ItemStackCount().ToString();
        }
        else
        {
            Inventory.instance.Remove(item);
        }
    }

}