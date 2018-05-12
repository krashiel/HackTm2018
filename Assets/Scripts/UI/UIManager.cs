using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    public GameObject inventory;
    public GameObject playerHealthBar;
    public GameObject enemyHealthBar;
    public GameObject playerInventoryObject;
    public GameObject craftingMenuObject;

    void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("warning more than one ui manager instances found");
            return;
        }
        Instance = this;

        SetPlayerInventoryActive(false);
        //SetInventoryActive(false);
        SetPlayerHealthBarActive(false);
        SetEnemyHealthBarActive(false);
        SetCraftingMenuActive(false);
    }

    //public void SetInventoryActive(bool status)
    //{
    //    inventory.SetActive(status);
    //}

    public void SetPlayerInventoryActive(bool status)
    {
        playerInventoryObject.SetActive(status);
    }

    public void SetCraftingMenuActive(bool status)
    {
        craftingMenuObject.SetActive(status);
    }

    public void SetPlayerHealthBarActive(bool status)
    {
        playerHealthBar.SetActive(status);
    }

    public void SetEnemyHealthBarActive(bool status)
    {
        enemyHealthBar.SetActive(status);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.I))
        {
            SetPlayerInventoryActive(!playerInventoryObject.activeSelf);
        }
        if (Input.GetKey(KeyCode.C))
        {
            SetCraftingMenuActive(!craftingMenuObject.activeSelf);
        }
    }
}
