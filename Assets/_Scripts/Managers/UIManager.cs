using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text currencyText;

    public void SetCurrency(int currency)
    {
        this.currencyText.text = "Currency: " + currency.ToString();
    }
}
