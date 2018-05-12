using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject inventory;
    public GameObject playerHealthBar;
    public GameObject enemyHealthBar;
    public GameObject playerInventoryObject;
    public GameObject craftingMenuObject;

    void Awake()
    {
        SetPlayerInventoryActive(false);
        //SetInventoryActive(false);
        SetPlayerHealthBarActive(true);
        SetEnemyHealthBarActive(true);
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
        //CraftRecipes.Instance.InitialiseCraftRecipesView();
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
        if (Input.GetKeyDown(KeyCode.I))
        {
            SetPlayerInventoryActive(!playerInventoryObject.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetCraftingMenuActive(!craftingMenuObject.activeSelf);
            craftingMenuObject.GetComponent<CraftRecipes>().InitialiseCraftRecipesView();
        }
    }
}
