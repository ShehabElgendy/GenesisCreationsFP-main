using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopSlot : MonoBehaviour
{
    [SerializeField]
    private Image itemSprite;

    [SerializeField]
    private TextMeshProUGUI itemName;

    [SerializeField]
    private TextMeshProUGUI itemCount;

    [SerializeField]
    private ShopSlot assignedItemSlot;

    [SerializeField]
    private Button addItemToCart;

    [SerializeField]
    private Button removeItemFromCart;

    public ShopSlot AssignedItemSlot => assignedItemSlot;

    private int tempAmount;

    public UIShopKeeperDisplay parentDisplay { get; private set; }


    private void Awake()
    {
        itemSprite.sprite = null;
        itemSprite.preserveAspect = true;
        itemSprite.color = Color.clear;
        itemName.text = string.Empty;
        itemCount.text = string.Empty;

        addItemToCart?.onClick.AddListener(AddItemToCart);
        removeItemFromCart?.onClick.AddListener(RemoveItemFromCart);

        parentDisplay = transform.parent.GetComponentInParent<UIShopKeeperDisplay>();
    }

    public void Init(ShopSlot _slot)
    {
        assignedItemSlot = _slot;
        tempAmount = _slot.StackSize;
        UpdateUISlot();
    }

    private void UpdateUISlot()
    {
        if (assignedItemSlot.ItemData != null)
        {
            itemSprite.sprite = assignedItemSlot.ItemData.Icon;
            itemSprite.color = Color.white;
            itemCount.text = assignedItemSlot.StackSize.ToString();
            var _modifiedPrice = UIShopKeeperDisplay.GetModifiedPrice(assignedItemSlot.ItemData, 1);
            itemName.text = $"{assignedItemSlot.ItemData.DisplayName} - x{_modifiedPrice}";
        }
        else
        {
            itemSprite.sprite = null;
            itemSprite.preserveAspect = true;
            itemSprite.color = Color.clear;
            itemName.text = string.Empty;
            itemCount.text = string.Empty;
        }
    }

    private void RemoveItemFromCart()
    {
        if(tempAmount == assignedItemSlot.StackSize) return;
        tempAmount++;
        parentDisplay.RemoveItemfromCart(this);
        itemCount.text = tempAmount.ToString();
    }

    private void AddItemToCart()
    {
        if (tempAmount <= 0) return;
        tempAmount--;
        parentDisplay.AddItemToCart(this);
        itemCount.text = tempAmount.ToString();
    }
}
