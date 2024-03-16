using System.Collections;
using TMPro;
using Unity.VisualScripting;
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

    private UIATMController aTMController;

    private BedInteraction bedInteraction;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        movement = FindObjectOfType<MovementController>();
        aTMController = FindObjectOfType<UIATMController>();
        bedInteraction = FindObjectOfType<BedInteraction>();
    }

    private void OnEnable()
    {
        ShopKeeper.OnShopWindowRequested += DisplayShopWindow;
        ATMInteraction.OnATMWindowRequested += DisplayATMWindow;
        BedInteraction.OnBedInteractionRequested += BedInteractionSleep;
    }

    private void Update()
    {
        coinsTxt.text = PlayerController.instance.Coins.ToString();
    }

    private void BedInteractionSleep(PlayerController _player)
    {
        movement.MovementAbility(false);
        AnimatorController.instance.FadeIn();
        bedInteraction.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(WaitForFade());
    }
    IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(6f);
        aTMController.Credit += aTMController.Credit * 10 / 100;
        if (aTMController.Credit == 0)
            aTMController.Credit += 100;

        bedInteraction.GetComponent<BoxCollider>().enabled = true;
        movement.MovementAbility(true);
    }

    private void DisplayATMWindow(PlayerController _player)
    {
        aTMWindow.gameObject.SetActive(!aTMWindow.activeSelf);
        movement.MovementAbility(!aTMWindow.activeSelf);
        LockCursor(!aTMWindow.activeSelf);
    }

    private void DisplayShopWindow(ShopSystem _shopSystem, PlayerInventoryHolder _playerInventory)
    {
        shopKeeperDisplay.gameObject.SetActive(!shopKeeperDisplay.gameObject.activeSelf);
        movement.MovementAbility(!shopKeeperDisplay.gameObject.activeSelf);
        LockCursor(!shopKeeperDisplay.gameObject.activeSelf);
        shopKeeperDisplay.DisplayShopWindow(_shopSystem, _playerInventory);
    }


    private void OnDisable()
    {
        ShopKeeper.OnShopWindowRequested -= DisplayShopWindow;
        ATMInteraction.OnATMWindowRequested -= DisplayATMWindow;
        BedInteraction.OnBedInteractionRequested -= BedInteractionSleep;
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
