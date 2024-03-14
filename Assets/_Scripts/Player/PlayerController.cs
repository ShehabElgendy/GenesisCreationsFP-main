using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public GameObject bedInteraction;

    private MovementController movement;

    public int coins;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        movement = GetComponent<MovementController>();
    }

    private void Start()
    {
        PlayerPrefs.SetInt(GameStatics.Coins, 1000);
        coins = PlayerPrefs.GetInt(GameStatics.Coins);
    }
    public void Sleep()
    {
        if (Input.GetKeyDown(KeyCode.E) && bedInteraction.gameObject.activeSelf)
        {
            movement.MovementAbility(false);
            bedInteraction.gameObject.SetActive(false);
            AnimatorController.instance.FadeIn();
            StartCoroutine(WaitForFade());
        }
    }

    IEnumerator WaitForFade()
    {
        yield return new WaitForSeconds(6f);
        coins += coins * 10 / 100;
        if (coins == 0)
        {
            coins += 100;
        }
        bedInteraction.SetActive(true);
        movement.MovementAbility(true);
    }
}
