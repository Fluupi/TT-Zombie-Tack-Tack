using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ColorOption : MonoBehaviour
{
    [SerializeField] private Button button;
    private Color color;
    public Color Color => color;

    public void SetColor(Color _color)
    {
        color = _color;
        button.image.color = color;
    }
}
