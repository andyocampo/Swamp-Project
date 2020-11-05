using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainingToolsHandler : MonoBehaviour
{
    [SerializeField]GameObject playerEditor;
    public static event Action<string> RemainingTools1Triggered;
    public static event Action<string> RemainingTools2Triggered;
    void Update()
    {
        PlayerLevelEditor playerLevelEditor = playerEditor.GetComponent<PlayerLevelEditor>();
        string RemainingTool1Text;
        string RemainingTool2Text;
        if (transform.GetChild(0))
        {
            RemainingTool1Text = $"{playerLevelEditor.tilesRemaining[0]}x";
            if (RemainingTools1Triggered != null)
            {
                RemainingTools1Triggered.Invoke(RemainingTool1Text);
            }
        }
        if (transform.GetChild(1))
        {
            RemainingTool2Text = $"{playerLevelEditor.tilesRemaining[1]}x";
            if (RemainingTools2Triggered != null)
            {
                RemainingTools2Triggered.Invoke(RemainingTool2Text);
            }
        }
    }
}
