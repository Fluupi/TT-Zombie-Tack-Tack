using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameMode", menuName = "ScriptableObjects/GameMode")]
public class GameMode : ScriptableObject
{
    [Header("Building")]
    public int BuildingPartClickNeed_1;
    public int BuildingPartClickNeed_2;
    public int BuildingPartClickNeed_3;
    [Header("Orange")]
    public float orangeTimerDuration;
    public float orangeMinEnemyMoveDuration;
    public float orangeMaxEnemyMoveDuration;
    [Header("Red")]
    public float redTimerDuration;
    public float redMinEnemyMoveDuration;
    public float redMaxEnemyMoveDuration;
}
