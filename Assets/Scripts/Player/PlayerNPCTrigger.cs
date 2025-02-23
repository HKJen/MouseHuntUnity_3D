using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNPCTrigger : MonoBehaviour
{
    [SerializeField] private MerchantDisplayer merchantDisplayer;
    [SerializeField] private DialogDisplayer dialogDisplayer;
    private PlayerUI playerUI;
    
    void Start()
    {
        playerUI = GetComponent<PlayerUI>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (playerUI.IsInInventory || merchantDisplayer.IsWithMerchant 
                                   || dialogDisplayer.IsInDialog || !Input.GetKey(KeyCode.E))
            return;

        if (other.TryGetComponent(out NPCMerchant merchant))
        {
            merchantDisplayer.SetMerchantItems(merchant.Items);
        }
        else if (other.TryGetComponent(out NPCDialog npc))
        {
            dialogDisplayer.StartDialog(npc);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (dialogDisplayer.IsInDialog)
            dialogDisplayer.CloseDialog();
        
        if (merchantDisplayer.IsWithMerchant)
            merchantDisplayer.CloseMerchant();
    }
}














