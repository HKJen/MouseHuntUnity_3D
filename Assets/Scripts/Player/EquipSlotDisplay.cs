using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipSlotDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text nameTMP;
    [SerializeField] private Image iconImg;

    public ItemType EquipType => equipType;
    [SerializeField] private ItemType equipType; 

    [SerializeField] private Image borders;
    [SerializeField] private Color selecetedColor;

    private int ind;
    private Color defColor;

    private PlayerEquipment equip;

    public void Setup(PlayerEquipment equipment, int index)
    {
        equip = equipment;
        ind = index;
        defColor = borders.color;
    }

    public void Select()
    {
        borders.color = selecetedColor;
        equip.SelectSlot(ind);
    }

    public void Deselect()
    {
        borders.color = defColor;
    }

    public void Equip(ItemSO item)
    {
        if (!item)
        {
            nameTMP.text = "Empty";
            iconImg.color = new Color(0, 0, 0, 0);
            return;
        }

        nameTMP.text = item.ItemName;
        iconImg.color = Color.white;
        iconImg.sprite = item.ItemIcon;
    }
}
