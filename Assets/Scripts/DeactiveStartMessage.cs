using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeactiveStartMessage : MonoBehaviour
{
    TMP_Text tmPro;

    private void Start()
    {
        tmPro = GetComponent<TMP_Text>();
    }
    // Update is called once per frame
    void Update()
    {
        if(tmPro.color.a <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}
