using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BuildingPart : MonoBehaviour
{
    private Image image;
    private Color baseColor;

    [SerializeField] private int maxLifePoints;
    [SerializeField] private int currentLifePoints;
    public bool IsAlive => currentLifePoints > 0;

    [SerializeField] private int moneyGain;
    public int MoneyGain => moneyGain;
    [SerializeField] private int clickNeed;
    private int currentClicks;
    public bool IsComplete => currentClicks == clickNeed;
    public UnityEvent OnCompletion;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Start()
    {
        UpdateDisplay();
    }

    public void Explode()
    {
        currentClicks = 0;
        currentLifePoints = maxLifePoints;
        UpdateDisplay();
    }

    public void SetColor(Color color)
    {
        color.a = 0;
        baseColor = color;
    }

    private void UpdateDisplay()
    {
        Color color = baseColor * (currentLifePoints * 1f / maxLifePoints);
        color.a = currentClicks * 1f / clickNeed;

        image.color = color;
    }

    public void AddClick()
    {
        currentClicks++;
        UpdateDisplay();

        if(IsComplete)
            OnCompletion.Invoke();
    }

    public void TakeDamage()
    {
        currentLifePoints--;
        UpdateDisplay();
    }
}
