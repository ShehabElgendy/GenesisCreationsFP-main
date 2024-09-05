using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIShoppingCartItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI itemText;


    internal void SetItemText(string _newString) => itemText.text = _newString;
}
