using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryHolder : InventoryHolder
{
    internal bool CheckInventoryRemaining(Dictionary<InventoryItemData, int> _shoppingCart)
    {
        var _clonedSystem = new InventorySystem(playerInventorySystem.InventorySize);

        for (int i = 0; i < playerInventorySystem.InventorySize; i++)
        {
            _clonedSystem.InventorySlots[i].AssignItem(playerInventorySystem.InventorySlots[i].ItemData, playerInventorySystem.InventorySlots[i].StackSize);
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
        if(playerInventorySystem.AddToInventory(_data, _amount)) return true;

        return false;
    }

}
