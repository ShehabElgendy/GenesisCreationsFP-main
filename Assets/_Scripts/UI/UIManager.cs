using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField]
    private TextMeshProUGUI coinsTxt;

    [SerializeField]
    private UIShopKeeperDisplay shopKeeperDisplay;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        ShopKeeper.OnShopWindowRequested += DisplayShopWindow;
    }

    private void DisplayShopWindow(ShopSystem _shopSystem, PlayerInventoryHolder _playerInventory)
    {
        shopKeeperDisplay.gameObject.SetActive(!shopKeeperDisplay.gameObject.activeSelf);
        PlayerController.instance.GetComponent<MovementController>().MovementAbility(!shopKeeperDisplay.gameObject.activeSelf);
        LockCursor(!shopKeeperDisplay.gameObject.activeSelf);
        shopKeeperDisplay.DisplayShopWindow(_shopSystem, _playerInventory);
    }

    private void OnDisable()
    {
        ShopKeeper.OnShopWindowRequested -= DisplayShopWindow;
    }

    void Start()
    {
        coinsTxt.text = PlayerController.instance.coins.ToString();
    }

    private void Update()
    {
        coinsTxt.text = PlayerController.instance.coins.ToString();
    }

    private void LockCursor(bool _newState)
    {
        if (_newState)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }
}
