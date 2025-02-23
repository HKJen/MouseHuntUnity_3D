using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Survival/Recipe")]

public class RecipeSO : ScriptableObject
{
    public int CraftCount;
    public ItemSO CraftedItem;

    public ItemSO[] Items;
    public int[] ItemsCount;
}
