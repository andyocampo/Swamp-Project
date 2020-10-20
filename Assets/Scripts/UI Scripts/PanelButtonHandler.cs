using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelButtonHandler : MonoBehaviour
{
    [SerializeField]
    GameObject Panel;

    public void OpenPanel() //opens and closes UI panel
    {
        Animator animator = Panel.GetComponent<Animator>();
        bool isOpen = animator.GetBool("is Open");

        animator.SetBool("is Open", !isOpen);
    }
}
