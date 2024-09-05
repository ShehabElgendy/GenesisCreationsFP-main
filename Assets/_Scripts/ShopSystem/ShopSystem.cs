using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ShopSystem
{
    [SerializeField]
    private List<ShopSlot> shopInventory;

    public List<ShopSlot> ShopInventory => shopInventory;


    public ShopSystem(int _size) => SetShopSize(_size);

    private void SetShopSize(int _size)
    {
        shopInventory = new List<ShopSlot>(_size);

        for (int i = 0; i < _size; i++)
        {
            shopInventory.Add(new ShopSlot());
        }
    }

    public void AddToShop(InventoryItemData _data, int _amount)
    {
        if (ContainsItem(_data, out ShopSlot _shopSlot))
        {
            _shopSlot.AddToStack(_amount);
            return;
        }

        var _freeSlot = GetFreeSlot();
        _freeSlot.AssignItem(_data, _amount);
    }

    private ShopSlot GetFreeSlot()
    {
        var _freeSlot = shopInventory.FirstOrDefault(i => i.ItemData == null);

        if (_freeSlot == null)
        {
            _freeSlot = new ShopSlot();
            shopInventory.Add(_freeSlot);
        }

        return _freeSlot;
    }

    public bool ContainsItem(InventoryItemData _itemToAdd, out ShopSlot _shopSlot)
    {
        _shopSlot = shopInventory.Find(i => i.ItemData == _itemToAdd);

        return _shopSlot != null;
    }

    internal void PurchaseItem(InventoryItemData _data, int _amount)
    {
        if (!ContainsItem(_data, out ShopSlot _shopSlot)) return;

        _shopSlot.RemoveFromStack(_amount);
    }

    internal void SellItem(InventoryItemData _kvpKey, int _kvpValue, int _price)
    {
        AddToShop(_kvpKey, _kvpValue);
    }
}
