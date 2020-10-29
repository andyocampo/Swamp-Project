using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinLoseController : MonoBehaviour
{
    private TMP_Text messageText;
    void Start()
    {
        messageText = GetComponentInChildren<TMP_Text>();
        messageText.text = $"You Win";
    }

    private void UpdateUI(string text)
    {
        messageText.text = text;
    }

    private void OnWinLoseTriggered(string text, int active)
    {
        var canvas = gameObject.GetComponent<CanvasGroup>();
        UpdateUI(text);
        canvas.alpha = active;
    }

    private void OnEnable()
    {
        WinLoseHandler.WinLoseTriggered += OnWinLoseTriggered;
    }

    private void OnDisable()
    {
        WinLoseHandler.WinLoseTriggered -= OnWinLoseTriggered;
    }
}
