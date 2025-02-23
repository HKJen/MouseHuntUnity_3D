using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MerchantSlotDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text nameTMP;
    [SerializeField] private TMP_Text costTMP;
    [SerializeField] private Image iconImg;

    [SerializeField] private Image borders;
    [SerializeField] private Color selectedColor;

    private MerchantDisplayer displayer;
    private int ind;
    private Color defColor;

    private ItemSO curItem;

    public void Setup(MerchantDisplayer disp, int index)
    {
        displayer = disp;
        ind = index;
        defColor = borders.color;
    }

    public void Select()
    {
        borders.color = selectedColor;
        displayer.SelectSlot(ind);
    }

    public void Deselect()
    {
        borders.color = defColor;
    }

    public void SetItem(ItemSO item)
    {
        nameTMP.text = item.ItemName;
        iconImg.color = Color.white;
        iconImg.sprite = item.ItemIcon;
        curItem = item;
    }

    public void SetIsBuying(bool isNowBuying)
    {
        if (isNowBuying)
            costTMP.text = curItem.BuyCost.ToString();
        else
            costTMP.text = curItem.SellCost.ToString();
    }

}
