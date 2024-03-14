using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ShopTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject ShopSt;

    private ShopSystem shopSystem;
    private PlayerInventoryHolder playerInventoryHolder;

    private UIShopKeeperDisplay UIShopKeeperDisplay;

    private void Awake()
    {
        playerInventoryHolder = FindObjectOfType<PlayerInventoryHolder>();
        UIShopKeeperDisplay = FindObjectOfType<UIShopKeeperDisplay>();
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.CompareTag("Player"))
        {
            ToggleVisibility();
            UnlockCursor();
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        ToggleVisibility();
        LockCursor();
    }
    public void ToggleVisibility()
    {
        ShopSt.SetActive(!ShopSt.activeSelf);
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