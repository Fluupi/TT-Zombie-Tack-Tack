using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class CoinCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        GameManager.Instance.OnMoneyChange.AddListener(UpdateText);
    }

    public void UpdateText(int money)
    {
        text.text = money.ToString();
    }
}
