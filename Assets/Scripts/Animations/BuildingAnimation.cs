using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildingAnimation : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Building building;
    [SerializeField] private GameObject[] buildingParts;

    [Header(("Animation Settings"))]
    [SerializeField] private float supposedGameDuration;
    private float timeSinceStart;
    [SerializeField] private AnimationCurve curve;

    [SerializeField] [Range(1.1f,2f)] private float pointerAmplificator;
    [SerializeField] private float slidingFactor;
    [SerializeField] private float nextPartFactor;
    [SerializeField] [Tooltip("In % (supposed duration)")] private float period;
    [SerializeField] private float shakeFactor;
    [SerializeField] private float partDelay;

    private void Start()
    {
        buildingParts = building.partGO();
    }

    public void ResetTime()
    {
        timeSinceStart = 0;
    }

    private void Update()
    {
        timeSinceStart += Time.deltaTime;

        float progress = timeSinceStart / supposedGameDuration;

        for (int i = 0; i < buildingParts.Length; i++)
        {
            
        }
    }
}
