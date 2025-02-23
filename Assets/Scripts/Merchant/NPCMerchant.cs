using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMerchant : MonoBehaviour
{
    public ItemSO[] Items => items;
    [SerializeField] private ItemSO[] items;
}
