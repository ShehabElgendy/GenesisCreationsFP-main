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
        _coins = PlayerController.instance.coins;
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
}
