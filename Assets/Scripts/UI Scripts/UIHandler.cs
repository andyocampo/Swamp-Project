using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{

    //observer pattern that tells current tool UI what tool is chosen;
    public static event Action<int> CurrentToolTriggered;
    public static bool deleteOn;

    private void Start()
    {
        deleteOn = false;
    }

    //opens and closes UI panel
    public void OpenPanel() 
    {
        Animator animator = gameObject.GetComponent<Animator>();
        bool isOpen = animator.GetBool("is Open");

        animator.SetBool("is Open", !isOpen);
    }

    //restarts level
    public void RestartOnClick() 
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentLevel);
    }

    //starts game and sets time to normal
    public void PlayOnClick() 
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }

    //pauses game movement
    public void PauseOnClick() 
    {
        Time.timeScale = 0;
    }

    //sets game to slow motion
    public void SlowOnClick() 
    {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    //sets game to 2x the speed
    public void FastOnClick() 
    {
        Time.timeScale = 2;
    }

    [SerializeField] Image deleteOffImage;
    //turns on/off the delete mode
    public void DeleteModeOnClick()
    {
        bool spriteEnabled = !deleteOn;
        deleteOn = !deleteOn;
        deleteOffImage.enabled = !spriteEnabled; 
    }

    //sets current tool to spring
    public void SpringOnClick()
    {
        if (CurrentToolTriggered != null)
        {
            CurrentToolTriggered.Invoke(0);
        }
    }

    //sets current tool to wall
    public void WallOnClick()
    {
        if (CurrentToolTriggered != null)
        {
            CurrentToolTriggered.Invoke(1);
        }
    }
}
