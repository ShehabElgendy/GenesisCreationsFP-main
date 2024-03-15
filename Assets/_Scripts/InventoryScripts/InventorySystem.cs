using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventorySystem
{
    [SerializeField]
    private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => inventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int _size)
    {
        CreateInventory(_size);
    }

    public InventorySystem(int _size, int _coins)
    {
        _coins = PlayerController.instance.Coins;
    }

    private void CreateInventory(int _size)
    {
        inventorySlots = new List<InventorySlot>(_size);

        for (int i = 0; i < _size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData _itemToAdd, int _amountToAdd)
    {
        if (ContainsItem(_itemToAdd, out List<InventorySlot> _slots))
        {
            foreach (var _slot in _slots) //Check
            {
                if (_slot.EnoughRoomLeftInStack(_amountToAdd))
                {
                    _slot.AddToStack(_amountToAdd);
                    OnInventorySlotChanged?.Invoke(_slot);

                    return true;
                }
            }

        }

        if (HasFreeSlot(out InventorySlot _freeSlot))
        {
            _freeSlot.UpdateInventorySlot(_itemToAdd, _amountToAdd);
            OnInventorySlotChanged?.Invoke(_freeSlot);
            return true;
        }

        return false;
    }

    public bool ContainsItem(InventoryItemData _itemToAdd, out List<InventorySlot> _slots)
    {
        _slots = InventorySlots.Where(i => i.ItemData == _itemToAdd).ToList();

        return _slots == null ? false : true;
    }

    public bool HasFreeSlot(out InventorySlot _freeSlot)
    {
        _freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return _freeSlot == null ? false : true;
    }

    internal Dictionary<InventoryItemData, int> GetAllItemsHeld()
    {
        var _distinctItems = new Dictionary<InventoryItemData, int>();

        foreach (var _item in inventorySlots)
        {
            if (_item.ItemData == null) continue;

            if (!_distinctItems.ContainsKey(_item.ItemData))
                _distinctItems.Add(_item.ItemData, _item.StackSize);
            else
                _distinctItems[_item.ItemData] += _item.StackSize;
        }

        return _distinctItems;
    }

    internal void AddCoinsFromShop(int _price)
    {
        PlayerController.instance.Coins += _price;
    }

    internal void RemoveItemsFromInventory(InventoryItemData _data, int _amount)
    {
        if (ContainsItem(_data, out List<InventorySlot> _invSlot))
        {
            foreach (var _slot in _invSlot)
            {
                var _stackSize = _slot.StackSize;

                if (_stackSize > _amount)
                {
                    _slot.RemoveFromStack(_amount);
                }
                else
                {
                    _slot.RemoveFromStack(_stackSize);
                    _amount -= _stackSize;
                }

                OnInventorySlotChanged?.Invoke(_slot);
            }
        }
    }
}
