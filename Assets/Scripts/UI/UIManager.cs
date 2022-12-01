using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Button backgroundColorChangeButton;

    [Header("Game")]
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Building building;
    [SerializeField] private Button buildingColorChangeButton;

    [Header("End")]
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject winImageGO;
    [SerializeField] private GameObject loseImageGO;

    private void Start()
    {
        ShowMenu();
    }

    public void UpdateColor(Color color)
    {
        menuPanel.GetComponent<Image>().color = color;
    }

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        endPanel.SetActive(false);
    }

    public void ShowGame()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        endPanel.SetActive(false);
    }

    public void ShowEnd(bool victory)
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        endPanel.SetActive(true);

        winImageGO.SetActive(victory);
        loseImageGO.SetActive(!victory);
    }
}
