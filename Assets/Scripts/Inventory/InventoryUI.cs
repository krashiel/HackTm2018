using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;
    public Transform inventorySlots;
    Inventory inventory;

    InventorySlot[] slots;

    void Start()
    {
        if (Instance != null)
        {
            Debug.Log("Warning, inventory ui instance already existing");
        }
        Instance = this;
        inventory = Inventory.instance;
        inventory.onItemAddedCallback += UpdateUI;

        slots = inventorySlots.GetComponentsInChildren<InventorySlot>();
    }

    public void RefreshUI()
    {
        foreach (var slot in slots)
        {
            slot.UpdateStackText(slot.item);
        }
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
