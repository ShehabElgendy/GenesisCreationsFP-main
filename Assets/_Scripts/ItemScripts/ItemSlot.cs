using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSlot
{
    [SerializeField]
    protected InventoryItemData itemData;

    [SerializeField]
    protected int itemID = -1;

    [SerializeField]
    protected int stackSize;

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    public void ClearSlot()
    {
        itemData = null;
        itemID = -1;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot _invSlot)
    {
        if(itemData == _invSlot.itemData)
        {
            AddToStack(_invSlot.stackSize);
        }
        else
        {
            itemData = _invSlot.itemData;
            itemID = itemData.ID;
            stackSize = 0;
            AddToStack(_invSlot.stackSize);
        }
    }

    public void AssignItem(InventoryItemData _data, int _amount)
    {
        if (itemData == _data)
        {
            AddToStack(_amount);
        }
        else
        {
            itemData = _data;
            itemID = _data.ID;
            stackSize = 0;
            AddToStack(_amount);
        }
    }

    public void AddToStack(int _amount) => stackSize += _amount;

    public void RemoveFromStack(int _amount)
    {
        stackSize -= _amount;

        if (stackSize <= 0)
        {
            ClearSlot();
        }
    }
}
