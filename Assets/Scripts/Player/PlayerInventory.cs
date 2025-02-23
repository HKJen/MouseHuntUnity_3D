using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform contentParent;
    [SerializeField] private int slotsCount;

    [SerializeField] private Canvas invCanvas;

    public ItemSO[] Items => itemsData; 
    private ItemSO[] itemsData;
    private int[] itemsCount;
    private InvSlotDisplay[] itemDisplayers;

    private int curSlot;

    private PlayerEquipment playerEquipment;
    private PlayerUI playerUI;

    void Start()
    {
        playerEquipment = GetComponent<PlayerEquipment>();
        playerUI = GetComponent<PlayerUI>();

        itemsData = new ItemSO[slotsCount];
        itemsCount = new int[slotsCount];
        itemDisplayers = new InvSlotDisplay[slotsCount];

        for (int i = 0; i < slotsCount; i++)
        {
            InvSlotDisplay invSlotDisplay = Instantiate(slotPrefab, contentParent).GetComponent<InvSlotDisplay>();
            invSlotDisplay.SetItem(null);
            //ADD
            invSlotDisplay.Setup(this, i);
            // FINISH ADD
            itemDisplayers[i] = invSlotDisplay;
        } 

        invCanvas.enabled = false;
        curSlot = -1;
    }

    public void SelectSlot(int ind)
    {
        if (itemsData[ind] == null)
        {
            itemDisplayers[ind].Deselect();
            return;
        }

        if (curSlot != -1)
            itemDisplayers[curSlot].Deselect();

        if (curSlot == ind)
        {
            if (itemsData[curSlot].ItemType == ItemType.Backpack || itemsData[curSlot].ItemType == ItemType.Tool)
            {
                ItemSO temp = itemsData[curSlot];

                itemsData[curSlot] = null;
                itemsCount[curSlot] = 0;
                itemDisplayers[curSlot].SetItem(null);

                playerEquipment.EquipItem(temp, curSlot);
            }
            curSlot = -1;
        }
        else
        {
            curSlot = ind;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Called Inventory from player Inventory");
            //invCanvas.enabled =! invCanvas.enabled;

            if (invCanvas.enabled)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if(playerUI.IsInInventory && Input.GetMouseButtonDown(1) && curSlot >= 0 && itemsData[curSlot] != null)
        {
            Instantiate(itemsData[curSlot].ItemObject, transform.position + transform.forward * 3 + transform.up, Quaternion.identity);

            if (itemsCount[curSlot] > 1)
            {
                itemsCount[curSlot]--;
                itemDisplayers[curSlot].UpdateItemCount(itemsCount[curSlot]);

                Group();
                Sort();
            }
            else
            {
                itemsData[curSlot] = null;
                itemsCount[curSlot] = 0;
                itemDisplayers[curSlot].SetItem(null);

                itemDisplayers[curSlot].Deselect();
                curSlot = -1;

                Sort();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Item item))
        {
            /*
            for (int i = 0; i < itemsData.Length; i++)
            {
                if(itemsData[i] == item.ItemData && itemsCount[i] < itemsData[i].ItemsInStack)
                {
                    itemsCount[i]++;
                    itemDisplayers[i].UpdateItemCount(itemsCount[i]);
                    break;
                }
                else if (!itemsData[i])
                {
                    itemsData[i] = item.ItemData;
                    itemsCount[i] = 1;
                    itemDisplayers[i].SetItem(item.ItemData);
                    itemDisplayers[i].UpdateItemCount(1);
                    break;
                }
                else if (i == itemsData.Length - 1)
                {
                    return;
                }
            }
            Destroy(item.gameObject);
            */

            int cell = GetSuitableCell(item.ItemData);
            if(cell >= 0)
            {
                AddItem(item.ItemData, cell);
                Destroy(item.gameObject);
            }
        }
    }

    public void Sort()
    {
        for(int i = 0; i < itemsData.Length; i++)
        {
            if(itemsData[i] != null)
                continue;
            
            for(int j = i + 1; j < itemsData.Length; j++)
            {
                if(itemsData[j] != null)
                {
                    itemsData[i] = itemsData[j];
                    itemsCount[i] = itemsCount[j];

                    itemDisplayers[i].SetItem(itemsData[i]);
                    itemDisplayers[i].UpdateItemCount(itemsCount[i]);

                    itemsData[j] = null;
                    itemsCount[j] = 0;

                    itemDisplayers[j].SetItem(null);

                    break;
                }
            }
        }

        if (curSlot >= 0 && itemsData[curSlot] == null)
        {
            itemDisplayers[curSlot].Deselect();
            curSlot = -1;
        }
    }

    public void Group()
    {
        for(int i = 0; i < itemsData.Length; i++)
        {
            if(itemsData[i] == null)
                continue;
            
            if(itemsCount[i] == itemsData[i].ItemsInStack)
                continue;

            for(int j = i + 1; j < itemsData.Length; j++)
            {
                if(itemsData[j] != null && itemsData[j] == itemsData[i])
                {
                    if(itemsCount[i] + itemsCount[j] > itemsData[i].ItemsInStack)
                    {
                        itemsCount[j] -= (itemsData[i].ItemsInStack - itemsCount[i]);
                        itemDisplayers[j].UpdateItemCount(itemsCount[j]);

                        itemsCount[i] = itemsData[i].ItemsInStack;
                        itemDisplayers[i].UpdateItemCount(itemsCount[i]);
                    }
                    else
                    {
                        itemsCount[i] += itemsCount[j];
                        itemDisplayers[i].UpdateItemCount(itemsCount[i]);

                        itemsData[j] = null;
                        itemsCount[j] = 0;
                        itemDisplayers[j].SetItem(null);
                    }

                    if(itemsData[i].ItemsInStack == itemsCount[i])
                        break;
                }
            }
        }
    }

    public int GetItemCount(ItemSO item)
    {
        int count = 0;

        for(int i = 0; i < itemsData.Length; i++)
        {
            if(itemsData[i] == item)
            {
                count += itemsCount[i];
            }
        }

        return count;
    }

    public void ChangeItemCount(ItemSO item, int newCount)
    {
        for(int i = 0; i < itemsData.Length; i++)
        {
            if(newCount == 0)
                return;
            
            if(itemsData[i] == item)
            {
                if(itemsCount[i] < newCount)
                {
                    newCount -= itemsCount[i];
                    itemsCount[i] = 0;
                }
                else
                {
                    itemsCount[i] -= newCount;
                    newCount = 0;
                }

                itemDisplayers[i].UpdateItemCount(itemsCount[i]);

                if(itemsCount[i] == 0)
                {
                    itemDisplayers[i].SetItem(null);
                    itemsData[i] = null;
                }
            }
        }
    }

    public int GetSuitableCell(ItemSO item)
    {
        for(int i = 0; i < itemsData.Length; i++)
        {
            if(itemsData[i] == item && itemsCount[i] < itemsData[i].ItemsInStack)
            {
                return i;
            }
            else if (!itemsData[i])
            {
                return i;
            }
            else if(i == itemsData.Length - 1)
            {
                return -1;
            }
        }
        return -1;
    }

    public void AddItem(ItemSO item, int cellInd)
    {
        if(itemsData[cellInd] == item && itemsCount[cellInd] < itemsData[cellInd].ItemsInStack)
        {
            itemsCount[cellInd]++;
            itemDisplayers[cellInd].UpdateItemCount(itemsCount[cellInd]);
        }
        else if(!itemsData[cellInd])
        {
            itemsData[cellInd] = item;
            itemsCount[cellInd] = 1;
            itemDisplayers[cellInd].SetItem(item);
            itemDisplayers[cellInd].UpdateItemCount(1);
        }
    }
}







