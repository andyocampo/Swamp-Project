using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    private int frogsDead;
    public int FrogsDead
    {
        get { return frogsDead; }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Frog"))
        {
            Destroy(other.gameObject);
            frogsDead++;
            FrogSpawner.amountOfFrogs--;
        }
    }
}
