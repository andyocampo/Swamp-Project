using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinLoseController : MonoBehaviour
{
    [SerializeField] TMP_Text messageText;
    void Start()
    {
        messageText.GetComponent<TMP_Text>();
        messageText.text = $"You Win";
    }

    private void UpdateUI(string text)
    {
        messageText.text = text;
    }

    public void OpenPanel()
    {
        Animator animator = gameObject.GetComponent<Animator>();
        bool isOpen = animator.GetBool("is Open");

        animator.SetBool("is Open", !isOpen);
    }

    private void OnWinLoseTriggered(string text)
    {
        UpdateUI(text);
        OpenPanel();
    }

    private void OnEnable()
    {
        WinLoseHandler.WinLoseTriggered += OnWinLoseTriggered;
    }

    private void OnDisable()
    {
        WinLoseHandler.WinLoseTriggered -= OnWinLoseTriggered;
    }
}
