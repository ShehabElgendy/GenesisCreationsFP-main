using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UIInventoryDisplay : MonoBehaviour
{
    protected InventorySystem inventorySystem;

    protected Dictionary<UIInventorySlot, InventorySlot> slotDictionary;

    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<UIInventorySlot, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {

    }

    public abstract void AssignSlots(InventorySystem _invToDisplay);

    protected virtual void UpdateSlot(InventorySlot _updatedSlot)
    {
        foreach (var _slot in slotDictionary)
        {
            if (_slot.Value == _updatedSlot)
            {
                _slot.Key.UpdateUISlot(_updatedSlot);
            }
        }
    }
}
