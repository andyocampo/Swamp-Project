using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseHandler : MonoBehaviour
{
    //Eventually these will probably be moved to a singleton
    [SerializeField] GameObject goal;
    [SerializeField] GameObject hazards;
    private int alive;
    private int dead;


    public static event Action<string> WinLoseTriggered;

    void Update()
    {
        alive = goal.GetComponent<EndGoal>().FrogGoal;
        dead = hazards.GetComponent<Hazard>().FrogsDead;

        string WinText;

        if (alive >= FrogSpawner.amountOfFrogs) //bad way of checking win
        {
            WinText = "You Win!";
            if (WinLoseTriggered != null)
            {
                WinLoseTriggered.Invoke(WinText);
            }
        }
        else if (dead >= FrogSpawner.amountOfFrogs)
        {
            WinText = "You Lose";
            if (WinLoseTriggered != null)
            {
                WinLoseTriggered.Invoke(WinText);
            }
            Time.timeScale = 0;
        }
    }
}