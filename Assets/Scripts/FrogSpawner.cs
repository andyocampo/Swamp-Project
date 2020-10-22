using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject frog;

    public int setAmountOfFrogs;
    public static int amountOfFrogs;
    private int frogSpawned = 0;

    private void Awake()
    {
        Time.timeScale = 0;
        amountOfFrogs = setAmountOfFrogs;
    }

    private void Update()
    {
        if(Time.timeScale > 0)
        {
            if (frogSpawned < amountOfFrogs) //checks if frogs spawned 
            {
                StartCoroutine(SpawnFrog());
            }
        }

    }

    IEnumerator SpawnFrog() //spawns a frog every four seconds
    {
        byte greenValue = (byte)(Random.Range(130, 255)); //gives each frog unique green color
        yield return new WaitForSeconds(3.5f);
        GameObject newFrog = Instantiate<GameObject>(frog, this.transform.position, Quaternion.identity);
        newFrog.GetComponent<SpriteRenderer>().material.color = new Color32(0, greenValue, 0, 255); //changes frog color
        frogSpawned++;
        StopAllCoroutines();
    }
}
