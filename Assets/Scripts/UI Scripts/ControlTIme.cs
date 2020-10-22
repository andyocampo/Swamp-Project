using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTIme : MonoBehaviour
{

    public void PlayOnClick()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }

    public void PauseOnClick()
    {
        Time.timeScale = 0;
    }

    public void SlowOnClick()
    {
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public void FastOnClick()
    {
        Time.timeScale = 2;
    }

}
