using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrogsRemainingController : MonoBehaviour
{
    private TMP_Text messageText;
    void Start()
    {
        messageText = GetComponent<TMP_Text>();
        messageText.text = $"Frogs: 0/{FrogSpawner.amountOfFrogs}";
    }

    private void UpdateUI(string text)
    {
        messageText.text = text;
    }

    private void OnFrogReachesGoalTriggered(string text)
    {
        UpdateUI(text);
    }

    private void OnEnable()
    {
        EndGoal.FrogReachesGoalTriggered += OnFrogReachesGoalTriggered;
    }

    private void OnDisable()
    {
        EndGoal.FrogReachesGoalTriggered -= OnFrogReachesGoalTriggered;
    }
}
