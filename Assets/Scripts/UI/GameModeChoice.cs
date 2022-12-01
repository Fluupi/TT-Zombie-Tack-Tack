using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameModeChoice : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [Space]
    [SerializeField] private GameMode[] gameModeList;
    [SerializeField] private int currentGameMode;

    private void Start()
    {
        GameManager.Instance.GameMode = gameModeList[currentGameMode];
    }

    public void ChangeGameMode()
    {
        currentGameMode = (++currentGameMode % gameModeList.Length);

        text.text = $"Play with mode :\n{gameModeList[currentGameMode].name}";
        GameManager.Instance.GameMode = gameModeList[currentGameMode];
    }
}
