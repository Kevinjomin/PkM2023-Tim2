using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level3DDisplayer : MonoBehaviour
{
    [SerializeField] private Levels levelInfo;

    [SerializeField] private TextMeshPro title;
    [SerializeField] private TextMeshPro score;

    [SerializeField] private GameObject blocker;
    [SerializeField] private GameObject interact;
    [SerializeField] private GameObject panel;

    [SerializeField] private Material successStar;

    [SerializeField] private List<GameObject> stars = new List<GameObject>();

    public void Initialize(Levels info)
    {
        levelInfo = info;

        title.text = levelInfo.GetLevelName();
        score.text = "High Score : " + levelInfo.GetScore();

        RatingChecker();
    }

    private void RatingChecker()
    {
        int rating = levelInfo.GetRating();

        for (int i = 0; i < rating; i++)
        {
            stars[i].GetComponent<MeshRenderer>().material = successStar;
        }
    }
    public void SwitchScene()
    {
        LevelManager.instance.activeLevel = levelInfo;
        SceneManager.LoadScene(levelInfo.GetSceneID());
    }
}
