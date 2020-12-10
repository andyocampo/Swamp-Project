using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Clip { OnClick, FrogJump, Goal };

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

    private AudioSource[] sounds;

    void Start()
    {
        instance = this;
        sounds = GetComponents<AudioSource>();
    }

    public void PlaySound(Clip audioClip)
    {
        sounds[(int)audioClip].Play();
    }

    public void ChangeOnClickPitch(float pitch)
    {
        sounds[(int)Clip.OnClick].pitch = pitch;
    }
}
