using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCraft : MonoBehaviour
{
    [SerializeField] private RecipeSO[] recipes;
    [SerializeField] private GameObject recipePrefab;
    [SerializeField] private Transform contentParent;

    private PlayerInventory inventory;

    void Start()
    {
        inventory = GetComponent<PlayerInventory>();

        for(int i = 0; i < recipes.Length; i++)
        {
            RecipeDisplayer recipe = Instantiate(recipePrefab, contentParent).GetComponent<RecipeDisplayer>();
            recipe.Setup(this, i, recipes[i]);
        }
    }

    public void CraftItem(int ind)
    {
        for(int i = 0; i < recipes[ind].Items.Length; i++)
        {
            if(inventory.GetItemCount(recipes[ind].Items[i]) < recipes[ind].ItemsCount[i])
            {
                return;
            }
        }

        int cell = inventory.GetSuitableCell(recipes[ind].CraftedItem);
        if(cell >= 0)
        {
            for(int i = 0; i < recipes[ind].Items.Length; i++)
            {
                inventory.ChangeItemCount(recipes[ind].Items[i], recipes[ind].ItemsCount[i]);
            }

            inventory.AddItem(recipes[ind].CraftedItem, cell);
            
            inventory.Group();
            inventory.Sort();

            Debug.Log("CREATED " + recipes[ind].CraftedItem.ItemName);
        }
    }
}
