using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  

public class RecipeItemInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text countTMP;
    [SerializeField] private Image iconImg;

    public void Setup(ItemSO item, int count)
    {
        countTMP.text = count.ToString();
        iconImg.sprite = item.ItemIcon;
    }

}
