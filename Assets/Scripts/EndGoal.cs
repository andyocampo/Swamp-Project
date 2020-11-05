using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGoal : MonoBehaviour
{

    private string frogsRemainingText;
    private int frogsWhoReachedGoal = 0; //frogs that have crossed the goal
    private int frogAmount; //amount of frogs in level
    public int FrogGoal 
    {
            get { return frogsWhoReachedGoal; }
    }

    private ParticleSystem goalParticles; //confetti particle system

    //observer pattern that tells frog remaining UI how many frogs left
    public static event Action<string> FrogReachesGoalTriggered;

    private void Start()
    {
        frogAmount = FrogSpawner.amountOfFrogs;
        goalParticles = GetComponentInChildren<ParticleSystem>();
        frogsRemainingText = $"Frogs: {frogsWhoReachedGoal}/{frogAmount}";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Frog"))
        {
            goalParticles.Play(); //launches confetti
            Destroy(other.gameObject); //destroys frog
            frogsWhoReachedGoal++;

            frogsRemainingText = $"Frogs: {frogsWhoReachedGoal}/{frogAmount}";

            if (FrogReachesGoalTriggered != null)
            {
                FrogReachesGoalTriggered.Invoke(frogsRemainingText);
            }
        }
    }
}
