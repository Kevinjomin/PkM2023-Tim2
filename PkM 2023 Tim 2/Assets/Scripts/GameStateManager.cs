using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject endPanel;

    [SerializeField] private ScoreSystem scoreSystem;

    public bool noninlevel;

    public enum GameState
    {
        START,
        INGAME,
        END
    }
    public GameState gameState;

    private void Awake()
    {
        if (!noninlevel)
            ChangeState(0);
    }
    private void Update()
    {
        if (noninlevel)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.L)) // FOR TESTING PURPOSES
            ChangeState(2);
        if (scoreSystem.trashToCollect <= 0)
            ChangeState(2);

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
