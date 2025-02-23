using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private EquipSlotDisplay[] equipSlots;
    [SerializeField] private GameObject[] equipObjects;
    private PlayerInventory inventory;

    private ItemSO[] equipedItems;

    private int curSelected = -1;
    
    void Start()
    {
        inventory = GetComponent<PlayerInventory>();

        for (int i = 0; i < equipObjects.Length; i++)
        {
            equipObjects[i].SetActive(false);
        }

        equipedItems = new ItemSO[equipSlots.Length];
        for (int i = 0; i < equipSlots.Length; i++)
        {
            equipSlots[i].Setup(this, i);
            equipSlots[i].Equip(null);
        }
        curSelected = -1;
    }

    public void SelectSlot(int ind)
    {
        if (curSelected != -1)
            equipSlots[curSelected].Deselect();

        if (curSelected == ind)
        {
            curSelected = -1;
        }
        else
        {
            curSelected = ind;
        }
    }

    public void EquipItem(ItemSO item, int fromCell)
    {
        for (int i = 0; i < equipSlots.Length; i++)
        {
            if (equipSlots[i].EquipType == item.ItemType)
            {
                if (equipedItems[i] != null)
                {
                    for (int j = 0; j < equipObjects.Length; j++)
                    {
                        if (equipObjects[j].name == equipedItems[i].EquippedName)
                        {
                            equipObjects[j].SetActive(false);
                            break;
                        }
                    }
                    inventory.AddItem(equipedItems[i], fromCell);
                }
                equipSlots[i].Equip(item);
                equipedItems[i] = item;

                for (int j = 0; j < equipObjects.Length; j++)
                {
                    if (equipObjects[j].name == equipedItems[i].EquippedName)
                    {
                        equipObjects[j].SetActive(true);
                        break;
                    }
                }

                inventory.Sort();
                return;
            }
        }
    }

}
