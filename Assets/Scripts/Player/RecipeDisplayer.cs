using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeDisplayer : MonoBehaviour
{
    [SerializeField] private TMP_Text nameTMP;
    [SerializeField] private TMP_Text countTMP;
    [SerializeField] private Image iconImg;

    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform contentParent;

    private PlayerCraft craft;
    private int ind;

    public void Setup(PlayerCraft plCraft, int index, RecipeSO recipe)
    {
        craft = plCraft;
        ind = index;

        nameTMP.text = recipe.CraftedItem.ItemName;
        iconImg.sprite = recipe.CraftedItem.ItemIcon;
        countTMP.text = recipe.CraftCount.ToString();

        for (int i = 0; i < recipe.Items.Length; i++)
        {
            RecipeItemInfo recipeItem = Instantiate(itemPrefab, contentParent).GetComponent<RecipeItemInfo>();
            recipeItem.Setup(recipe.Items[i], recipe.ItemsCount[i]);
        }
    }

    public void Craft()
    {
        craft.CraftItem(ind);
    }
}
