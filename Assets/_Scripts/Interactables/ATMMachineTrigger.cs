using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATMMachineTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject ATMMachBG;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<Interactor>())
        {
            ATMMachBG.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.GetComponent<Interactor>())
        {
            ATMMachBG.SetActive(false);
        }
    }
}
