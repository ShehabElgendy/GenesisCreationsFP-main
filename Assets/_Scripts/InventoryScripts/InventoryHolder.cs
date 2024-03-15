using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField]
    private int inventorySize;

    [SerializeField]
    protected InventorySystem playerInventorySystem;

    public InventorySystem PlayerInventorySystem => playerInventorySystem;

    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;

    protected virtual void Awake()
    {
        playerInventorySystem = new InventorySystem(inventorySize);
    }
}
