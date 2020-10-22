using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public void RestartOnClick()
    {
        SceneManager.LoadScene(0);
    }

    public void PlayOnClick() //starts game and sets time to normal
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }

    public void PauseOnClick() //pauses game movement
    {
        Time.timeScale = 0;
    }

    public void SlowOnClick() //sets game to slow motion
    {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public void FastOnClick() //sets game to 2x the speed
    {
        Time.timeScale = 2;
    }
}
