using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATMMachine : MonoBehaviour
{
    [SerializeField]
    private GameObject ATMMach;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           ToggleVisibility();
            UnlockCursor();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ToggleVisibility();
        LockCursor();
    }
    public void ToggleVisibility()
    {
        ATMMach.SetActive(!ATMMach.activeSelf);
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private static void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
