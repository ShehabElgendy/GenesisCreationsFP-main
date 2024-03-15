using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public static AnimatorController instance;


    private Animator anim;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        anim = GetComponent<Animator>();
    }

    public void FadeIn()
    {
        anim.SetBool(GameStatics.FadeScreen, true);
    }

    public void FadeOut()
    {
        anim.SetBool(GameStatics.FadeScreen, false);
    }
}
