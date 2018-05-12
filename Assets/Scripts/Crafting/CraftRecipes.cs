using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftRecipes : MonoBehaviour
{
    public static CraftRecipes Instance;

    public GameObject craftRecipesParent;
    public List<CraftRecipe> craftingRecipes;

    private void Start()
    {
        if (Instance != null)
        {
            Debug.Log("warning there is already an instance of craft recipes");
            return;
        }
        Instance = this;
    }

    public void InitialiseCraftRecipesView()
    {
        var parentRecipes = craftRecipesParent.GetComponentsInChildren<CraftRecipe>();
        for (int i = 0; i < parentRecipes.Length; i++)
        {
            parentRecipes[i].checkIfEligible(false);
            craftingRecipes.Add(parentRecipes[i]);
        }
    }
}
