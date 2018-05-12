using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform inventorySlots;
    Inventory inventory;

    InventorySlot[] slots;

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemAddedCallback += UpdateUI;

        slots = inventorySlots.GetComponentsInChildren<InventorySlot>();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
        Debug.Log("Updating ui");
    }
}
