using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ItemPickUp : MonoBehaviour
{
    public InventoryItemData ItemData;

    private SphereCollider myCollider;

    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider _other)
    {
        var _inventory = _other.GetComponent<PlayerInventoryHolder>();

       if(!_inventory) return;

       if (_inventory.PlayerInventorySystem.AddToInventory(ItemData, 1))
        {
            Destroy(gameObject);
        }
    }
}
