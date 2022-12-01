using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Image background;
    [SerializeField] private Button backgroundColorChangeButton;

    [Header("Game")]
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Building building;
    [SerializeField] private Button buildingColorChangeButton;

    private void Start()
    {
        ShowMenu();
    }

    public void UpdateColor(Color color)
    {
        menuPanel.GetComponent<Image>().color = color;
    }

    public void ShowGame()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
    }
}
