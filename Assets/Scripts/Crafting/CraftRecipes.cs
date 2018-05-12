using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftRecipes : MonoBehaviour
{

    public GameObject craftRecipesParent;
    public List<CraftRecipe> craftingRecipes;

    public void initialiseCraftRecipesView()
    {
        var parentRecipes = craftRecipesParent.GetComponentsInChildren<CraftRecipe>();
        for (int i = 0; i < parentRecipes.Length; i++)
        {
            parentRecipes[i].checkIfEligible(false);
            craftingRecipes.Add(parentRecipes[i]);
        }
    }
}
