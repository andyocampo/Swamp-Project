using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGoal : MonoBehaviour
{

    private string frogsRemainingText;
    private int frogGoal = 0;
    private int frogAmount = 0; //amount of frogs in level
    public int FrogGoal 
    {
            get { return frogGoal; }
    }

    private ParticleSystem goalParticles; //confetti particle system

    //observer pattern that tells frog remaining UI how many frogs left
    public static event Action<string> FrogReachesGoalTriggered;

    private void Start()
    {
        frogAmount = FrogSpawner.amountOfFrogs;
        goalParticles = GetComponentInChildren<ParticleSystem>();
        frogsRemainingText = $"Frogs: {frogGoal}/{frogAmount}";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Frog")
        {
            goalParticles.Play(); //launches confetti
            Destroy(other.gameObject); //destroys frog
            frogGoal++;

            frogsRemainingText = $"Frogs: {frogGoal}/{frogAmount}";

            if (FrogReachesGoalTriggered != null)
            {
                FrogReachesGoalTriggered.Invoke(frogsRemainingText);
            }
        }
    }
}
