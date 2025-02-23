using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MerchantDisplayer : MonoBehaviour
{
    public bool IsWithMerchant => merchantCanvas.activeSelf;
    [SerializeField] private GameObject merchantCanvas;

    [SerializeField] private TMP_Text buttonText;
    private const string btnBuyText = "Purchase";
    private const string btnSellText = "Sell";

    [SerializeField] private Toggle buyToggle;

    [SerializeField] private Animator decorAnim;
    private const string animStr = "Deal";
    
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform contentParent;

    [SerializeField] private PlayerInventory playerInv;
    [SerializeField] private PlayerStats playerStats;
    
    private ItemSO[] buyItems;
    private ItemSO[] playerItems;
    private int curSelected;
    
    private bool isBuying;
    private List<MerchantSlotDisplay> merchantDisplayers = new List<MerchantSlotDisplay>();

    void Start()
    {
        curSelected = -1;
        merchantCanvas.SetActive(false);
    }

    public void SelectSlot(int ind)
    {
        if (curSelected != -1)
            merchantDisplayers[curSelected].Deselect();

        if (curSelected == ind)
        {
            curSelected = -1;
        }
        else
        {
            curSelected = ind;
        }
    }
    
    private void DisableUnusedSlots(int length)
    {
        if (merchantDisplayers.Count > length)
        {
            for (int i = length; i < merchantDisplayers.Count; i++)
            {
                merchantDisplayers[i].gameObject.SetActive(false);
            }
        }
    }

    private void SetupSlots(ItemSO[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (merchantDisplayers.Count <= i)
            {
                MerchantSlotDisplay merchantDisplay = Instantiate(slotPrefab, contentParent)
                    .GetComponent<MerchantSlotDisplay>();
                merchantDisplay.Setup(this, i);
                merchantDisplayers.Add(merchantDisplay);
            }
            else
            {
                if (!merchantDisplayers[i].gameObject.activeSelf)
                {
                    merchantDisplayers[i].gameObject.SetActive(true);
                }
            }
            merchantDisplayers[i].SetItem(items[i]);
            merchantDisplayers[i].SetIsBuying(isBuying);
        }
    }

    public void SetMerchantItems(ItemSO[] items)
    {
        buyItems = items;
        merchantCanvas.SetActive(true);
        
        buyToggle.isOn = true;

        SetToBuy(true);
        
        Cursor.lockState = CursorLockMode.None;
    }

    private void SetSlotsToBuy()
    {
        DisableUnusedSlots(buyItems.Length);
        SetupSlots(buyItems);
    }

    private void SetSlotsToSell()
    {
        if (playerInv.Items.Length == 0)
        {
            for (int i = 0; i < merchantDisplayers.Count; i++)
            {
                merchantDisplayers[i].gameObject.SetActive(false);
            }

            return;
        }
        
        List<ItemSO> tmpItems = new List<ItemSO>();

        for (int i = 0; i < playerInv.Items.Length; i++)
        {
            if (playerInv.Items[i] != null)
            {
                if (!tmpItems.Contains(playerInv.Items[i]))
                {
                    tmpItems.Add(playerInv.Items[i]);
                }
            }
            else
            {
                break;
            }
        }
        playerItems = tmpItems.ToArray();
        
        DisableUnusedSlots(tmpItems.Count);
        SetupSlots(playerItems);
    }

    public void SetToBuy(bool isNowBuying)
    {
        isBuying = isNowBuying;

        for (int i = 0; i < merchantDisplayers.Count; i++)
        {
            merchantDisplayers[i].SetIsBuying(isBuying);
        }

        if (isBuying)
        {
            buttonText.text = btnBuyText;
            SetSlotsToBuy();
        }

        else
        {
            buttonText.text = btnSellText;
            SetSlotsToSell();
        }

        if (curSelected != -1)
        {
            merchantDisplayers[curSelected].Deselect();
            curSelected = -1;
        }
    }

    public void CloseMerchant()
    {
        merchantCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MakeDeal()
    {
        if (curSelected == -1)
            return;

        if (isBuying)
        {
            if (playerStats.CurMoney < buyItems[curSelected].BuyCost)
                return;
            
            int cellInd = playerInv.GetSuitableCell(buyItems[curSelected]);
            
            if (cellInd < 0)
                return;
            
            playerInv.AddItem(buyItems[curSelected], cellInd);
            playerStats.GetMoney(-buyItems[curSelected].BuyCost);
            
            playerInv.Group();
            playerInv.Sort();
        }

        else
        {
            playerInv.ChangeItemCount(playerItems[curSelected], 1);
            playerStats.GetMoney(playerItems[curSelected].SellCost);
            
            playerInv.Group();
            playerInv.Sort();

            if (playerInv.GetItemCount(playerItems[curSelected]) == 0)
            {
                SetSlotsToSell();
                merchantDisplayers[curSelected].Deselect();
                curSelected = -1;
            }
        }
        
        decorAnim.SetTrigger(animStr);
    }
}








