using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject bedBG;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<Interactor>())
        {
            bedBG.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        bedBG.SetActive(false);
    }
}
