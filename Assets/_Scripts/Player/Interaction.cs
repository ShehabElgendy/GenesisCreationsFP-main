using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    private GameObject bedInteraction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameStatics.PlayeTag))
        {
            bedInteraction.SetActive(true);
            PlayerController.instance.Sleep();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(GameStatics.PlayeTag))
        {
            PlayerController.instance.Sleep();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        bedInteraction.SetActive(false);
    }
}
