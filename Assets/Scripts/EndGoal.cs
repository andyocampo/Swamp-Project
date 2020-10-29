using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGoal : MonoBehaviour
{

    private string frogsRemainingText;
    private int frogGoal = 1;
    private ParticleSystem goalParticles;

    private void Start()
    {
        goalParticles = GetComponentInChildren<ParticleSystem>();
    }


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
            goalParticles.Play(); //launches confetti
            Destroy(other.gameObject); //destroys frog
            frogGoal++;

            if (frogGoal == FrogSpawner.amountOfFrogs + 1)
            {
                frogsRemainingText = "You Win!";
            }

            if (FrogReachesGoalTriggered != null)
            {
                FrogReachesGoalTriggered.Invoke(frogsRemainingText);
            }
        }
    }
}
