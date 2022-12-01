using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region Singleton

    private static GameManager _instance;

    public static GameManager Instance => _instance;


    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            throw new System.Exception("An instance of this singleton already exists.");
        }
        else
        {
            _instance = this;
        }
    }

    #endregion

    [Header("References")]
    [SerializeField] private GameObject maleCharacter;
    [SerializeField] private GameObject femaleCharacter;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CountDown orangeCountDown;
    [SerializeField] private CountDown redCountDown;

    [Space]
    [SerializeField] private int money;
    [SerializeField] private int fastBuildMoneyGain;

    [Header("Game Cycle")]
    [SerializeField] private bool running;
    [SerializeField] private int clicks;

    public UnityEvent OnConstruct;
    public UnityEvent OnStartGame;
    public UnityEvent OnDamageTaken;
    public UnityEvent OnEndGame;
    public UnityEvent<int> OnMoneyChange;

    public bool Running => running;

    private void Start()
    {
        if (Random.Range(0, 2) == 0)
            maleCharacter.SetActive(false);
        else
            femaleCharacter.SetActive(false);
    }

    public void StartGame()
    {
        running = true;
        OnStartGame.Invoke();
    }

    public void Click()
    {
        OnConstruct.Invoke();
        clicks++;
    }

    public void WinEnd()
    {
        EndEnemies();
        CheckBuildingSpeed();
        EndGame();
    }

    public void LoseEnd()
    {
        EndEnemies();
        EndGame();
    }

    public void EndEnemies()
    {
        running = false;
    }

    public void CheckBuildingSpeed()
    {
        if (orangeCountDown.IsCounting && redCountDown.IsCounting)
        {
            money += fastBuildMoneyGain;
            OnMoneyChange.Invoke(money);
        }
    }

    public void EndGame()
    {
        OnEndGame.Invoke();
        clicks = 0;
    }

    public void BuildUp(BuildingPart buildingPart)
    {
        money += buildingPart.MoneyGain;
        OnMoneyChange.Invoke(money);
    }

    public bool CanSpend(int amount)
    {
        Debug.Log($"Money({money}), Checked Amount({amount})");
        return money >= amount;
    }

    public bool TryAndSpend(int amount)
    {
        if (!CanSpend(amount))
            return false;
        
        money -= amount;
        OnMoneyChange.Invoke(money);
        return true;
    }
}
