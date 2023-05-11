using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHelpMenu : MonoBehaviour
{
    public UIArrow arrow;
    bool isInitialArrowPlayed = false;

    public void Close()
    {
        FindObjectOfType<AudioManager>().Play("Close");
        // Activa flecha si es cierre inicial
        if (!isInitialArrowPlayed)
        {
            arrow.Play(5, .5f);
            isInitialArrowPlayed = true;
        }

        gameObject.SetActive(false);
    }    
}
