using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCursorOverUI : MonoBehaviour
{
    [SerializeField] GameObject cursor;

    public void OnMouseOver()
    {
        HideCursor();
    }

    public void OnMouseExit()
    {
        ShowCursor();
    }

    private void HideCursor()
    {
        cursor.SetActive(false);
    }

    private void ShowCursor()
    {
        cursor.SetActive(true);
    }

}
