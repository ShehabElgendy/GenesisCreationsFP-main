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

    [SerializeField]
    private int credit;

    private int creditValue = 3000;


    private void Start()
    {
        credit = creditValue;

        creditTxt.text = credit.ToString();
    }

    private void Update()
    {
        creditTxt.text = credit.ToString();
    }

    public void Deposit()
    {
        if (PlayerController.instance.Coins > depositAmount)
        {
            credit += depositAmount;
            PlayerController.instance.Coins -= depositAmount;
        }
        else
        {
            int remainingCredit = PlayerController.instance.Coins - PlayerController.instance.Coins;
            credit += PlayerController.instance.Coins; ;
            PlayerController.instance.Coins = remainingCredit;
        }


    }

    public void WithDraw()
    {
        if (credit > withdrawtAmount)
        {
            credit -= depositAmount;
            PlayerController.instance.Coins += withdrawtAmount;
        }
        else
        {
            int remainingCredit = credit;
            credit -= remainingCredit;
            PlayerController.instance.Coins += remainingCredit;

        }
    }
}
