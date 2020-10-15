using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGoal : MonoBehaviour
{

    private string frogsRemainingText;
    private int frogGoal = 1;


    void Update()
    {
        frogsRemainingText = $"Frogs: {frogGoal}/{FrogSpawner.amountOfFrogs}";
    }

    //observer pattern that tells frog remaining UI how many frogs left
    public static event Action<string> FrogReachesGoalTriggered; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Frog")
        {
            Destroy(other.gameObject); //destroys frog
            frogGoal++;
            if (FrogReachesGoalTriggered != null)
            {
                FrogReachesGoalTriggered.Invoke(frogsRemainingText);
            }
        }
    }
}
