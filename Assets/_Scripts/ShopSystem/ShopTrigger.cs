using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ShopTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject ShopBG;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<Interactor>())
        {
            ShopBG.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.GetComponent<Interactor>())
        {
            ShopBG.SetActive(false);
        }
    }
}