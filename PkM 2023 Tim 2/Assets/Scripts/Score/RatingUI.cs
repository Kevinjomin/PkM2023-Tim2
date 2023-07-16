using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RatingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ratingDescription;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private List<GameObject> Stars = new List<GameObject>();

    private void Start()
    {
        menuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.scene.MainMenuScene);
            SaveManager.instance.SaveGame();
        });
        nextLevelButton.onClick.AddListener(() =>
        {
            SaveManager.instance.SaveGame();
        });
    }
    public void DisplayRating(string description, int star)
    {
        ratingDescription.text = description;
        for (int i = 0; i < star; i++)
        {
            Stars[i].SetActive(true);
        }
    }
    public void WinCondition(bool win, Levels level)
    {
        if (win)
        {
            Levels nextLevel = level.GetNextLevel();

            nextLevelButton.interactable = true;
            nextLevelButton.onClick.AddListener(() =>
            {
                NextLevel(nextLevel);
            });
        }
        else
            nextLevelButton.interactable = false;
    }
    private void NextLevel(Levels nextLevel)
    {
        LevelManager.instance.activeLevel = nextLevel;
        SceneManager.LoadScene(nextLevel.GetSceneID());
    }
}
