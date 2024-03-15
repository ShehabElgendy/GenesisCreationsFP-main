using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public GameObject bedInteraction;

    public int Coins;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        PlayerPrefs.SetInt(GameStatics.Coins, 1000);
        Coins = PlayerPrefs.GetInt(GameStatics.Coins);
    }
    //public void Sleep()
    //{
    //    if (Input.GetKeyDown(KeyCode.E) && bedInteraction.gameObject.activeSelf)
    //    {
    //        movement.MovementAbility(false);
    //        bedInteraction.gameObject.SetActive(false);
    //        AnimatorController.instance.FadeIn();
    //        StartCoroutine(WaitForFade());
    //    }
    //}

    //IEnumerator WaitForFade()
    //{
    //    yield return new WaitForSeconds(6f);
    //    Coins += Coins * 10 / 100;
    //    if (Coins == 0)
    //    {
    //        Coins += 100;
    //    }
    //    bedInteraction.SetActive(true);
    //    movement.MovementAbility(true);
    //}
}
