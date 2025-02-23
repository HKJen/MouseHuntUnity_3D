using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BarrierScript : MonoBehaviour
{
    [SerializeField] private GameObject barrier;

    void Start()
    {
        barrier.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Barrier"))
        {
            barrier.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Barrier"))
        {
            barrier.SetActive(false);
        }
    }


}
