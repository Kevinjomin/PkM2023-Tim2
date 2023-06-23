using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Button MenuButton;

    public static bool isPaused = false;
    public GameObject pauseMenu;

    private void Awake()
    {
        // make event for every button clicks that changes the scene
        MenuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            isPaused = false;
            Loader.Load(Loader.scene.MainMenuScene);
        });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }


}
