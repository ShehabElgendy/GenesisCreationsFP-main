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


    private void Start()
    {
        PlayerPrefs.SetInt("Credit", 3000);
        credit = PlayerPrefs.GetInt("Credit");

        creditTxt.text = credit.ToString();
    }

    private void Update()
    {
        creditTxt.text = credit.ToString();
    }

    public void Deposit()
    {
        if (PlayerController.instance.coins > depositAmount)
        {
            credit += depositAmount;
            PlayerController.instance.coins -= depositAmount;
        }
        else
        {
            int remainingCredit = PlayerController.instance.coins - PlayerController.instance.coins;
            credit += PlayerController.instance.coins; ;
            PlayerController.instance.coins = remainingCredit;
        }


    }

    public void WithDraw()
    {
        if (credit > withdrawtAmount)
        {
            credit -= depositAmount;
            PlayerController.instance.coins += withdrawtAmount;
        }
        else
        {
            int remainingCredit = credit;
            credit -= remainingCredit;
            PlayerController.instance.coins += remainingCredit;

        }
    }
}
