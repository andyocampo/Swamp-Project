using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RemainingToolsController : MonoBehaviour
{
    private TMP_Text messageText;
    void Start()
    {
        messageText = GetComponent<TMP_Text>();
        messageText.text = $"# of Tools";
    }

    private void UpdateUI(string text)
    {
        messageText.text = text;
    }

    private void OnRemainingToolsTriggered(string text)
    {
        UpdateUI(text);
    }

    private void OnEnable()
    {
        if (name == "Tool 1 Remaining")
        {
            RemainingToolsHandler.RemainingTools1Triggered += OnRemainingToolsTriggered;
        }
        else if (name == "Tool 2 Remaining")
        {
            RemainingToolsHandler.RemainingTools2Triggered += OnRemainingToolsTriggered;
        }
    }

    private void OnDisable()
    {
        if (name == "Tool 1 Remaining")
        {
            RemainingToolsHandler.RemainingTools1Triggered -= OnRemainingToolsTriggered;
        }
        else if (name == "Tool 2 Remaining")
        {
            RemainingToolsHandler.RemainingTools2Triggered -= OnRemainingToolsTriggered;
        }
    }
}