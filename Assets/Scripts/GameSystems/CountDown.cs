using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private bool autoRelaunch;
    [SerializeField] private float timerDuration;
    private bool isCounting;
    private float currentTime;
    public float Progress => currentTime / timerDuration;
    public bool IsCounting => isCounting;

    [Header("Display needs")]
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Color color;
    [SerializeField] private bool showMM;
    [SerializeField] private bool showSS;
    [SerializeField] private bool showMS;
    private int lastMMValue;
    private int lastSSValue;

    public UnityEvent OnCountStart;
    public UnityEvent OnCountEnd;

    private void Start()
    {
        timerText.color = color;
        if(autoRelaunch)
            OnCountEnd.AddListener(Launch);
    }

    public void Launch()
    {
        ResetTimer();
        OnCountStart.Invoke();
        isCounting = true;
    }

    private void ResetTimer()
    {
        currentTime = 0;
        lastMMValue = 0;
        lastSSValue = 0;
    }

    public void UpdateText(string newText) => descriptionText.text = newText;

    private void Update()
    {
        if(!isCounting)
            return;

        currentTime += Time.deltaTime;

        if (currentTime >= timerDuration)
        {
            currentTime = timerDuration;
            isCounting = false;
            OnCountEnd.Invoke();
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        float count = timerDuration - currentTime;
        int mmValue = (int)(count / 60);
        count -= mmValue * 60;
        int ssValue = (int)(count);
        count -= ssValue;
        int msValue = (int)(count * 100);

        if (!showMS && (!showSS || ssValue == lastSSValue) && (!showMM || mmValue == lastMMValue))
        {
            lastSSValue = ssValue;
            lastMMValue = mmValue;

            return;
        }

        timerText.text = "";

        lastSSValue = ssValue;
        lastMMValue = mmValue;

        if (showMM)
        {
            timerText.text += $"{(mmValue < 10 ? "0" : "")}{mmValue}{(showSS || showMS ? ":":"")}";
        }

        if (showSS)
        {
            timerText.text += $"{(ssValue < 10 ? "0" : "")}{ssValue}{(showMS ? ":":"")}";
        }

        if (showMS)
        {
            timerText.text += $"{(msValue < 10 ? "0" : "")}{msValue}";
        }
    }
}
