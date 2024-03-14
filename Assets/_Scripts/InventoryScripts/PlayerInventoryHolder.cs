using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryHolder : InventoryHolder
{
    internal bool CheckInventoryRemaining(Dictionary<InventoryItemData, int> _shoppingCart)
    {
        var _clonedSystem = new InventorySystem(primaryInventorySystem.InventorySize);

        for (int i = 0; i < primaryInventorySystem.InventorySize; i++)
        {
            _clonedSystem.InventorySlots[i].AssignItem(primaryInventorySystem.InventorySlots[i].ItemData, primaryInventorySystem.InventorySlots[i].StackSize);
        }

        foreach (var _kvp in _shoppingCart)
        {
            for (int i = 0; i < _kvp.Value; i++)
            {
                if (!_clonedSystem.AddToInventory(_kvp.Key, 1)) return false;
            }
        }

        return true;
    }

    public bool AddToInventory(InventoryItemData _data, int _amount)
    {
        if(primaryInventorySystem.AddToInventory(_data, _amount)) return true;

        return false;
    }

}
