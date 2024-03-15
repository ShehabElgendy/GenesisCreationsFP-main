using TMPro;
using UnityEngine;

public class ATMController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI creditTxt;

    [SerializeField]
    private int depositAmount = 100;

    [SerializeField]
    private int withdrawtAmount = 100;

    public int Credit;

    private int creditValue = 3000;


    private void Start()
    {
        Credit = creditValue;

        creditTxt.text = Credit.ToString();
    }

    private void Update()
    {
        creditTxt.text = Credit.ToString();
    }

    public void Deposit()
    {
        if (PlayerController.instance.Coins > depositAmount)
        {
            Credit += depositAmount;
            PlayerController.instance.Coins -= depositAmount;
        }
        else
        {
            int remainingCredit = PlayerController.instance.Coins - PlayerController.instance.Coins;
            Credit += PlayerController.instance.Coins; ;
            PlayerController.instance.Coins = remainingCredit;
        }


    }

    public void WithDraw()
    {
        if (Credit > withdrawtAmount)
        {
            Credit -= depositAmount;
            PlayerController.instance.Coins += withdrawtAmount;
        }
        else
        {
            int remainingCredit = Credit;
            Credit -= remainingCredit;
            PlayerController.instance.Coins += remainingCredit;

        }
    }
}
