using UnityEngine;

[System.Serializable]
public class InventorySlot : ItemSlot
{
    public InventorySlot(InventoryItemData _data, int _amount)
    {
        itemData = _data;
        stackSize = _amount;
    }

    public InventorySlot() => ClearSlot();


    public bool EnoughRoomLeftInStack(int _amountToAdd, out int _amountRemaining)
    {
        _amountRemaining = itemData.MaxStackSize - stackSize;
        return EnoughRoomLeftInStack(_amountToAdd);
    }
    public bool EnoughRoomLeftInStack(int _amountToAdd)
    {
        if (itemData == null || itemData != null && stackSize + _amountToAdd <= itemData.MaxStackSize) return true;
        else return false;
    }

    public void UpdateInventorySlot(InventoryItemData _data, int _amount)
    {
        itemData = _data;
        stackSize = _amount;
    }


}