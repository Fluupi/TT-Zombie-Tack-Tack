using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("Options")]
    [SerializeField] private int optionColorsPrice;
    [SerializeField] private ColorOption backgroundColorOption;
    [SerializeField] private ColorOption buildingColorOption;

    [Header("Colors")]
    [SerializeField] private Color[] availableColors;

    public UnityEvent<Color> OnColorBackgroundChange;
    public UnityEvent<Color> OnColorBuildingChange;

    private void OnEnable()
    {
        UpdateChoice();
    }

    public void UpdateChoice()
    {
        UpdateColorChoice(backgroundColorOption);
        UpdateColorChoice(buildingColorOption);

        //Check if can buy at least one
        bool canBuyColor = GameManager.Instance.CanSpend(optionColorsPrice);

        backgroundColorOption.gameObject.GetComponent<Button>().interactable = canBuyColor;
        buildingColorOption.gameObject.GetComponent<Button>().interactable = canBuyColor;
    }

    private void UpdateColorChoice(ColorOption option)
    {
        int colorIndex = Random.Range(0, availableColors.Length);
        
        while (availableColors[colorIndex] == option.Color)
        {
            Debug.Log(colorIndex);
            colorIndex = Random.Range(0, availableColors.Length);
        }

        option.SetColor(availableColors[colorIndex]);
    }

    public void TryAndBuyBackgroundColor()
    {
        if (GameManager.Instance.TryAndSpend(optionColorsPrice))
        {
            OnColorBackgroundChange.Invoke(backgroundColorOption.Color);
        }
    }

    public void TryAndBuyBuildingColor()
    {
        if (GameManager.Instance.TryAndSpend(optionColorsPrice))
        {
            OnColorBuildingChange.Invoke(buildingColorOption.Color);
        }
    }
}
