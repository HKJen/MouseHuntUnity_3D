using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Canvas invCanvas;

    [SerializeField] private GameObject invGroup;
    [SerializeField] private GameObject craftGroup;

    [SerializeField] private DialogDisplayer dialogDisplayer;
    
    [SerializeField] private MerchantDisplayer merchantDisplayer;

    public bool IsInInventory => invCanvas.enabled;

    
    void Start()
    {
        craftGroup.SetActive(false);
        invCanvas.enabled = false;
    }

    public void ChooseFirstCategory(bool isFirst)
    {
        invGroup.SetActive(isFirst);
        craftGroup.SetActive(false);
    }

    public void ChooseSecondCategory(bool isSecond)
    {
        craftGroup.SetActive(isSecond);
        invGroup.SetActive(false);
    }

    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.I) && !dialogDisplayer.IsInDialog && !merchantDisplayer.IsWithMerchant)
        {
            Debug.Log("called inventory from PlayerUI");
            invCanvas.enabled = !invCanvas.enabled;

            if (invCanvas.enabled)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
