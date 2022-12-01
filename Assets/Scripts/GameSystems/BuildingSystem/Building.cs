using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Building : MonoBehaviour
{
    public Color Color;

    [SerializeField] private BuildingPart[] parts;
    private int currentPartInBuilding;

    public UnityEvent OnDestruction;
    public UnityEvent OnCompletion;

    private void Start()
    {
        UpdateColor(Color.blue);

        foreach (BuildingPart part in parts)
        {
            part.OnCompletion.AddListener(NextBuildingPart);
        }
    }

    public void Explode()
    {
        currentPartInBuilding = 0;

        foreach (BuildingPart part in parts)
        {
            part.Explode();
        }
    }

    public void UpdateColor(Color color)
    {
        Color = color;

        foreach (BuildingPart part in parts)
        {
            part.SetColor(color);
        }
    }

    public void Click()
    {
        if (currentPartInBuilding == parts.Length)
        {
            OnCompletion.Invoke();
            return;
        }

        parts[currentPartInBuilding].AddClick();
    }

    public void NextBuildingPart()
    {
        GameManager.Instance.BuildUp(parts[currentPartInBuilding]);
        currentPartInBuilding++;
    }

    public void TakeDamage()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            if (parts[i].IsAlive)
            {
                parts[i].TakeDamage();
                return;
            }
        }

        OnDestruction.Invoke();
    }
}
