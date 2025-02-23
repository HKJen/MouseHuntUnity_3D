using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    public DialogSO Dialog => dialog;
    [SerializeField] private DialogSO dialog;

    private Animator animator;

    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void UpdateDialogStatus(bool isInDialog)
    {
        animator.SetBool("IsInDialog", isInDialog);
    }

    public void SetAnimation(int ind)
    {
        animator.SetFloat("DialogInd", dialog.AnimInd[ind]);
    }


}
