using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject endPanel;

    [SerializeField] private ScoreSystem scoreSystem;

    public bool notinlevel;

    public enum GameState
    {
        START,
        INGAME,
        END
    }
    public GameState gameState;

    private void Awake()
    {
        if (!notinlevel)
            ChangeState(0);
        else
            Time.timeScale = 1f;
    }
    private void Update()
    {
        if (notinlevel)
        {
            return;
        }
        if (scoreSystem.trashToCollect <= 0)
        {
            if ((int)gameState == 2)
                return;
            ChangeState(2);
        }
    }
    public void ChangeState(int state)
    {
        gameState = (GameState)state;
        ChangePanel();
    }
    private void ChangePanel()
    {
        startPanel.SetActive(false);
        inGamePanel.SetActive(false);
        endPanel.SetActive(false);

        switch(gameState)
        {
            case GameState.START:
                startPanel.SetActive(true);
                Time.timeScale = 0f;
                break;
            case GameState.INGAME:
                inGamePanel.SetActive(true);
                Time.timeScale = 1f;
                break;
            case GameState.END:
                endPanel.SetActive(true);
                Time.timeScale = 0f;
                GameEnd();
                break;
        }
    }
    public void GameEnd()
    {
        scoreSystem.EndGame(); // Should be Event
    }
}
