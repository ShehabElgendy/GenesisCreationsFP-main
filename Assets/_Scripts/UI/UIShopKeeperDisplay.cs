using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopKeeperDisplay : MonoBehaviour
{
    [SerializeField]
    private UIShopSlot shopSlotPrefab;

    [SerializeField]
    private UIShoppingCartItem shoppingCartItemPrefab;

    [SerializeField]
    private Button buyTab;

    [SerializeField]
    private Button sellTab;

    [Header("Shopping Cart")]

    [SerializeField]
    private TextMeshProUGUI basketTotalTxt;

    [SerializeField]
    private TextMeshProUGUI playerCoinsTxt;

    [SerializeField]
    private Button buyButton;

    [SerializeField]
    private TextMeshProUGUI buyButtonTxt;

    [Header("Item Preview Section")]

    [SerializeField]
    private Image itemPreviewSprite;

    [SerializeField]
    private TextMeshProUGUI itemPreviewNameTxt;

    [SerializeField]
    private GameObject itemListContentPanel;

    [SerializeField]
    private GameObject shoppingCartContentPanel;

    private int basketTotal;

    private ShopSystem shopSystem;

    private PlayerInventoryHolder playerInventoryHolder;

    private UIShopSlot uiShopSlot;

    private InventorySystem inventorySystem;

    private bool isSelling;


    private Dictionary<InventoryItemData, int> shoppingCart = new();
    private Dictionary<InventoryItemData, UIShoppingCartItem> shoppingCartUI = new();


    public void DisplayShopWindow(ShopSystem _shopSystem, PlayerInventoryHolder _inventoryHolder)
    {
        shopSystem = _shopSystem;

        playerInventoryHolder = _inventoryHolder;

        RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        if (buyButton != null)
        {
            buyButtonTxt.text = isSelling ? "Sell Items" : "Buy Items";
            buyButton.onClick.RemoveAllListeners();

            if (isSelling) buyButton.onClick.AddListener(SellItems);
            else buyButton.onClick.AddListener(BuyItems);
        }

        ClearSlots();
        ClearItemPreview();

        basketTotalTxt.enabled = false;
        buyButton.gameObject.SetActive(false);
        basketTotal = 0;
        playerCoinsTxt.text = $"Coins: {PlayerController.instance.Coins}";

        if (isSelling)
            DisplayPlayerInventory();
        else
            DisplayShopInventory();
    }

    private void BuyItems()
    {
        if (PlayerController.instance.Coins < basketTotal) return;

        if (!playerInventoryHolder.CheckInventoryRemaining(shoppingCart)) return;

        foreach (var _kvp in shoppingCart)
        {
            shopSystem.PurchaseItem(_kvp.Key, _kvp.Value);

            for (int i = 0; i < _kvp.Value; i++)
            {
                playerInventoryHolder.PlayerInventorySystem.AddToInventory(_kvp.Key, 1); //need check
            }
        }

        SpendCoins(PlayerController.instance.Coins);

        RefreshDisplay();
    }

    private void SpendCoins(int _value)
    {
        _value = PlayerController.instance.Coins -= basketTotal;
    }

    private void SellItems()
    {
        foreach (var _kvp in shoppingCart)
        {

            var _price = GetModifiedPrice(_kvp.Key, _kvp.Value);

            shopSystem.SellItem(_kvp.Key, _kvp.Value, _price);

            playerInventoryHolder.PlayerInventorySystem.AddCoinsFromShop(_price);
            playerInventoryHolder.PlayerInventorySystem.RemoveItemsFromInventory(_kvp.Key, _kvp.Value);
        }

        RefreshDisplay();
    }

    private void ClearSlots()
    {
        shoppingCart = new();
        shoppingCartUI = new();

        foreach (var _item in itemListContentPanel.transform.Cast<Transform>())
        {
            Destroy(_item.gameObject);
        }

        foreach (var _item in shoppingCartContentPanel.transform.Cast<Transform>())
        {
            Destroy(_item.gameObject);
        }
    }

    private void DisplayShopInventory()
    {
        foreach (var _item in shopSystem.ShopInventory)
        {
            if (_item.ItemData == null) continue;

            var _shopSlot = Instantiate(shopSlotPrefab, itemListContentPanel.transform);
            _shopSlot.Init(_item);
        }
    }

    private void DisplayPlayerInventory()
    {
        foreach (var _item in playerInventoryHolder.PlayerInventorySystem.GetAllItemsHeld())
        {
            var _tempSlot = new ShopSlot();
            _tempSlot.AssignItem(_item.Key, _item.Value);

            var _shopSlot = Instantiate(shopSlotPrefab, itemListContentPanel.transform);
            _shopSlot.Init(_tempSlot);
        }
    }

    public void RemoveItemfromCart(UIShopSlot _uIShopSlot)
    {
        var _data = _uIShopSlot.AssignedItemSlot.ItemData;

        var _price = GetModifiedPrice(_data, 1);

        if (shoppingCart.ContainsKey(_data))
        {
            shoppingCart[_data]--;
            var _newString = $"{_data.DisplayName} ({_price}) x{shoppingCart[_data]}";
            shoppingCartUI[_data].SetItemText(_newString);

            if (shoppingCart[_data] <= 0)
            {
                shoppingCart.Remove(_data);

                var _tempObj = shoppingCartUI[_data].gameObject;

                shoppingCartUI.Remove(_data);

                Destroy(_tempObj);
            }
        }

        basketTotal -= _price;
        basketTotalTxt.text = $"Total: {basketTotal}";

        if (basketTotal <= 0 && basketTotalTxt.IsActive())
        {
            basketTotalTxt.enabled = false;
            buyButton.gameObject.SetActive(false);

            ClearItemPreview();
            return;
        }

        CheckCartVsAvailableCoins();
    }

    private void ClearItemPreview()
    {
        itemPreviewSprite.sprite = null;
        itemPreviewSprite.color = Color.clear;
        itemPreviewNameTxt.text = string.Empty;
    }

    internal void AddItemToCart(UIShopSlot _uIShopSlot)
    {
        var _data = _uIShopSlot.AssignedItemSlot.ItemData;

        UpdateItemPreview(_uIShopSlot);

        var _price = GetModifiedPrice(_data, 1);

        if (shoppingCart.ContainsKey(_data))
        {
            shoppingCart[_data]++;
            var _newString = $"{_data.DisplayName} ({_price}) x{shoppingCart[_data]}";
            shoppingCartUI[_data].SetItemText(_newString);
        }
        else
        {
            shoppingCart.Add(_data, 1);

            var _shoppingCartTextObj = Instantiate(shoppingCartItemPrefab, shoppingCartContentPanel.transform);
            var _newString = $"{_data.DisplayName} ({_price}) x1";
            _shoppingCartTextObj.SetItemText(_newString);
            shoppingCartUI.Add(_data, _shoppingCartTextObj);
        }

        basketTotal += _price;
        basketTotalTxt.text = $"Total: {basketTotal}";

        if (basketTotal > 0 && !basketTotalTxt.IsActive())
        {
            basketTotalTxt.enabled = true;
            buyButton.gameObject.SetActive(true);
        }

        CheckCartVsAvailableCoins();
    }

    private void CheckCartVsAvailableCoins()
    {
        var _coinsToCheck = PlayerController.instance.Coins;

        if (!isSelling)
            basketTotalTxt.color = basketTotal > _coinsToCheck ? Color.red : Color.white;

        if (isSelling || playerInventoryHolder.CheckInventoryRemaining(shoppingCart)) return;

        basketTotalTxt.text = "Not Enough Room in Inventory";
        basketTotalTxt.color = Color.red;
    }

    public static int GetModifiedPrice(InventoryItemData _data, int _amount)
    {
        return _data.Price * _amount;
    }

    private void UpdateItemPreview(UIShopSlot _uIShopSlot) //need to remove
    {
        var _data = _uIShopSlot.AssignedItemSlot.ItemData;

        itemPreviewSprite.sprite = _data.Icon;
        itemPreviewSprite.color = Color.white;
        itemPreviewNameTxt.text = _data.DisplayName;
    }

    public void OnBuyTabPressed()
    {
        isSelling = false;
        RefreshDisplay();
    }

    public void OnSellTabPressed()
    {
        isSelling = true;
        RefreshDisplay();
    }
}

