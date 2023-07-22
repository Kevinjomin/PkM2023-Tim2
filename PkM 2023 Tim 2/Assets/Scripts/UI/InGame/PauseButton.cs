using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{

    public void TogglePause()
    {
        if(PauseMenuUI.isPaused)
        {
            PauseMenuUI.pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
            PauseMenuUI.
        }
        else
        {

        }
    }
}
