using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [SerializeField]
    private TextMeshProUGUI coinsTxt;

    [SerializeField]
    private UIShopKeeperDisplay shopKeeperDisplay;

    [SerializeField]
    private GameObject aTMWindow;

    private MovementController movement;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        movement = FindObjectOfType<MovementController>();
    }

    private void OnEnable()
    {
        ShopKeeper.OnShopWindowRequested += DisplayShopWindow;
        ATMInteraction.OnATMWindowRequested += DisplayATMWindow;
        BedInteraction.OnBedInteractionRequested += BedInteractionSleep;
    }

    private void BedInteractionSleep(PlayerController arg0)
    {
        movement.MovementAbility(false);
        AnimatorController.instance.FadeIn();
        FindObjectOfType<BedInteraction>().GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(WaitForFade());
    }
    IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(6f);
        PlayerController.instance.Coins += PlayerController.instance.Coins * 10 / 100;
        if (PlayerController.instance.Coins == 0)
        {
            PlayerController.instance.Coins += 100;
        }
        FindObjectOfType<BedInteraction>().GetComponent<BoxCollider>().enabled = true;
        movement.MovementAbility(true);
    }

    private void DisplayATMWindow(PlayerController _player)
    {
        aTMWindow.gameObject.SetActive(!aTMWindow.activeSelf);
        PlayerController.instance.GetComponent<MovementController>().MovementAbility(!aTMWindow.activeSelf);
        LockCursor(!aTMWindow.activeSelf);
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
        ATMInteraction.OnATMWindowRequested -= DisplayATMWindow;
        BedInteraction.OnBedInteractionRequested -= BedInteractionSleep;
    }

    void Start()
    {
        coinsTxt.text = PlayerController.instance.Coins.ToString();
    }

    private void Update()
    {
        coinsTxt.text = PlayerController.instance.Coins.ToString();
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
