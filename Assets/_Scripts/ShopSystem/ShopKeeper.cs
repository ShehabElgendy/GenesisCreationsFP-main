using UnityEngine;
using UnityEngine.Events;

public class ShopKeeper : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ShopItemList shopItemHeld;

    [SerializeField]
    private ShopSystem shopSystem;

    public ShopSystem ShopSystem => shopSystem;

    public static UnityAction<ShopSystem, PlayerInventoryHolder> OnShopWindowRequested { get; set; }
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    private void Awake()
    {
        shopSystem = new ShopSystem(shopItemHeld.Items.Count);

        foreach (var item in shopItemHeld.Items)
        {
            shopSystem.AddToShop(item.ItemData, item.Amount);
        }
    }

    public void Interact(Interactor _interactor, out bool _interactionSuccefull)
    {
        var _playerInv = _interactor.GetComponent<PlayerInventoryHolder>();

        if (_playerInv != null)
        {

            OnShopWindowRequested?.Invoke(shopSystem, _playerInv);

            _interactionSuccefull = true;
        }
        else
        {
            _interactionSuccefull = false;
            Debug.LogError("Player Inventory not Found");
        }
    }
}
