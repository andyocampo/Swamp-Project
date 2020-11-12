using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseHandler : MonoBehaviour
{
    //Eventually these will probably be moved to a singleton
    [SerializeField] GameObject goal;
    GameObject[] hazards;
    GameObject[] enemies;

    private int alive;
    private int dead;
    private List<int> killedByEnemy;
    private List<int> killedByHazard;


    public static event Action<string> WinLoseTriggered;

    private void Start()
    {
        enemies = (GameObject.FindGameObjectsWithTag("Enemy"));
        hazards = (GameObject.FindGameObjectsWithTag("Hazard"));
        killedByEnemy = new List<int>();
        killedByHazard = new List<int>();

        for (int i = 0; i < enemies.Length; i++)
        {
            killedByEnemy.Add(enemies[i].GetComponent<Hazard>().FrogsDead);
        }

        for (int i = 0; i < hazards.Length; i++)
        {
            killedByHazard.Add(hazards[i].GetComponent<Hazard>().FrogsDead);
        }
    }

    void Update()
    {
        alive = goal.GetComponent<EndGoal>().FrogGoal;

        for (int i = 0; i < enemies.Length; i++)
        {
            killedByEnemy[i] = enemies[i].GetComponent<Hazard>().FrogsDead;
        }

        for (int i = 0; i < hazards.Length; i++)
        {
            killedByHazard[i] = hazards[i].GetComponent<Hazard>().FrogsDead;
        }

        dead = (killedByEnemy.Sum() + killedByHazard.Sum());

        string WinText;

        if (dead >= FrogSpawner.amountOfFrogs)
        {
            WinText = "You Lose";
            if (WinLoseTriggered != null)
            {
                WinLoseTriggered.Invoke(WinText);
            }
            Time.timeScale = 0;
        }
        else if (alive >= FrogSpawner.amountOfFrogs) //bad way of checking win
        {
            WinText = "You Win!";
            if (WinLoseTriggered != null)
            {
                WinLoseTriggered.Invoke(WinText);
            }
        }
        Debug.Log(dead);
    }
}