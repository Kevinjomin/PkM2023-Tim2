using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Button MenuButton;
    [SerializeField] private GameStateManager gameManager;
    [SerializeField] private GameObject settingsUI;

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
            LevelManager.instance.LevelFailed();
        });
    }

    void Update()
    {
        if(gameManager!= null)
        {
            if (gameManager.gameState == GameStateManager.GameState.INGAME)
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
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        settingsUI.SetActive(false);
    }


}
