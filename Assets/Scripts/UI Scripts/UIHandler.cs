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
    [SerializeField] Toggle deleteModeToggle;

    private void Start()
    {
        deleteOn = false;
        deleteModeToggle.GetComponent<Toggle>();
    }

    //opens and closes UI panel
    public void OpenPanel() 
    {
        Animator animator = gameObject.GetComponent<Animator>();
        bool isOpen = animator.GetBool("is Open");

        animator.SetBool("is Open", !isOpen);
        PlayOnClickSound(1.5f);
    }

    //restarts level
    public void RestartOnClick() 
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentLevel);
        PlayOnClickSound(1.5f);
    }

    //changes level
    public void NextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextLevel);
        PlayOnClickSound(1);
    }

    //starts game and sets time to normal
    public void PlayOnClick() 
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
        PlayOnClickSound(1);
    }

    //pauses game movement
    public void PauseOnClick() 
    {
        Time.timeScale = 0;
        PlayOnClickSound(1);
    }

    //sets game to slow motion
    public void SlowOnClick() 
    {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        PlayOnClickSound(.5f);
    }

    //sets game to 2x the speed
    public void FastOnClick() 
    {
        Time.timeScale = 2;
        PlayOnClickSound(2f);
    }

    [SerializeField] Image deleteOffImage;
    //turns on/off the delete mode
    public void DeleteModeOnClick()
    {
        bool spriteEnabled = !deleteOn;
        deleteOn = !deleteOn;
        deleteOffImage.enabled = !spriteEnabled;
        PlayOnClickSound(.5f);
    }

    //sets current tool to spring
    public void SpringOnClick()
    {
        if (CurrentToolTriggered != null)
        {
            deleteOffImage.enabled = true;
            deleteModeToggle.isOn = false;
            CurrentToolTriggered.Invoke(0);
        }
        PlayOnClickSound(1.5f);
    }

    //sets current tool to wall
    public void WallOnClick()
    {
        if (CurrentToolTriggered != null)
        {
            deleteOffImage.enabled = true;
            deleteModeToggle.isOn = false;
            CurrentToolTriggered.Invoke(1);
        }
        PlayOnClickSound(1.5f);
    }

    private void PlayOnClickSound(float pitch)
    {
        SoundManager.Instance.ChangeOnClickPitch(pitch);
        SoundManager.Instance.PlaySound(Clip.OnClick);
    }
}
