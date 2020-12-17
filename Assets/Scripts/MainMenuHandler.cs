using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
    public void Start()
    {
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
        SoundManager.Instance.PlaySound(Clip.OnClick);
    }

    //exits game
    public void Exit()
    {
        SoundManager.Instance.PlaySound(Clip.OnClick);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
    }
}
