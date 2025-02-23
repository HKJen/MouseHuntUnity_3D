using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private DialogDisplayer dialogDisplayer;
    private PlayerUI playerUI;

    void Start()
    {
        playerUI = GetComponent<PlayerUI>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!playerUI.IsInInventory && !dialogDisplayer.IsInDialog &&
            other.TryGetComponent(out NPCDialog npc) && Input.GetKey(KeyCode.E))
        {
            dialogDisplayer.StartDialog(npc);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        dialogDisplayer.CloseDialog();
    }
}
