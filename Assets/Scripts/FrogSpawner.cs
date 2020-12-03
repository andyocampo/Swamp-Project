using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject frog;

    [SerializeField] int setAmountOfFrogs;
    public static int amountOfFrogs; //ugly code, but eventually will probably be added to singleton
    private int frogSpawned = 0;

    private void Awake()
    {
        amountOfFrogs = setAmountOfFrogs;
        Time.timeScale = 0;
    }

    private void Update()
    {
        if(Time.timeScale > 0)
        {
            if (frogSpawned != setAmountOfFrogs) //checks if frogs spawned 
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
        FrogSpawnAnimation(newFrog);
        frogSpawned++;
        StopAllCoroutines();
    }

    private void FrogSpawnAnimation(GameObject frog)
    {
        Animator animator = frog.GetComponent<Animator>();
        bool isSpawned = animator.GetBool("is Spawned");

        animator.SetBool("is Spawned", !isSpawned);
    }
}
