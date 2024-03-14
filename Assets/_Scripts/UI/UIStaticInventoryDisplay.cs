using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIStaticInventoryDisplay : UIInventoryDisplay
{
    [SerializeField]
    private InventoryHolder inventoryHolder;

    [SerializeField]
    private UIInventorySlot[] slots;

    private void Awake()
    {
        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.PrimaryInventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }
        else
            Debug.LogWarning($"No Inventory assigned to {this.gameObject}");

        AssignSlots(inventorySystem);
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void AssignSlots(InventorySystem _invToDisplay)
    {
        slotDictionary = new Dictionary<UIInventorySlot, InventorySlot>();

        if (slots.Length != inventorySystem.InventorySize)
            Debug.Log($"Inventory slots out of sync on {this.gameObject}");

        for (int i = 0; i < slots.Length; i++)
        {
            slotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
            slots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }
}
