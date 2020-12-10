using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("Sound Manager does not exist!!!");
            }

            return instance;
        }
    }

    public void PlayPlayerClick()
    {
        
    }

    public void PlayFrogJump()
    {

    }

    public void PlayFrogGoal()
    {

    }
}
