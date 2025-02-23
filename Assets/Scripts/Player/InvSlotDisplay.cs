using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InvSlotDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text nameTMP;
    [SerializeField] private TMP_Text countTMP;
    [SerializeField] private Image iconImg;

    [SerializeField] private Image borders;
    [SerializeField] private Color selectedColor;

    private PlayerInventory inventory;
    private int ind;
    private Color defColor;

    public void Setup(PlayerInventory inv, int index)
    {
        inventory = inv;
        ind = index;
        defColor = borders.color;
    }

    public void Select()
    {
        borders.color = selectedColor;
        inventory.SelectSlot(ind);
    }

    public void Deselect()
    {
        borders.color = defColor;
    }

    public void SetItem(ItemSO item)
    {
        if (!item)
        {
            nameTMP.text = "Empty";
            countTMP.text = string.Empty;
            iconImg.color = new Color(0, 0, 0, 0);
            return;
        }
        nameTMP.text = item.ItemName;
        iconImg.color = Color.white;
        iconImg.sprite = item.ItemIcon;
    }

    public void UpdateItemCount(int newCount)
    {
        countTMP.text = newCount.ToString();
    }

    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
