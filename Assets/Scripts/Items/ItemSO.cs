using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Survival/Item")]

public class ItemSO : ScriptableObject
{
    public string ItemName;
    public Sprite ItemIcon;
    public int ItemsInStack;
    public ItemType ItemType;
    public string EquippedName;
    public GameObject ItemObject;
    public int SellCost;
    public int BuyCost;
}

public enum ItemType
{
    Item,
    Food,
    Medical,
    Backpack,
    Tool
}

