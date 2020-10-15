using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentToolTextController : MonoBehaviour
{
    private TMP_Text messageText;
    void Start()
    {
        messageText = GetComponent<TMP_Text>();
        messageText.text = $"Current Tool: Spring";
    }

    private void UpdateUI(string text)
    {
        messageText.text = text;
    }

    private void OnCurrentToolTriggered(string text)
    {
        UpdateUI(text);
    }

    private void OnEnable()
    {
        PlayerLevelEditor.CurrentToolTriggered += OnCurrentToolTriggered;
    }

    private void OnDisable()
    {
        PlayerLevelEditor.CurrentToolTriggered -= OnCurrentToolTriggered;
    }
}
