using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTIme : MonoBehaviour
{

    public void PlayOnClick()
    {
        Time.timeScale = 1;
    }

    public void PauseOnClick()
    {
        Time.timeScale = 0;
    }

    public void SlowOnClick()
    {
        Time.timeScale = 0.5f;
    }

    public void FastOnClick()
    {
        Time.timeScale = 2;
    }

}
