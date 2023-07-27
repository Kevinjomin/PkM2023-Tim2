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
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject indicator;

    [SerializeField] private Material successStar;
    [SerializeField] private Material unlocked;

    [SerializeField] private List<GameObject> stars = new List<GameObject>();

    [SerializeField] private Image bar;

    public void Initialize(Levels info)
    {
        levelInfo = info;

        title.text = levelInfo.GetLevelName();
        score.text = "High Score : " + levelInfo.GetScore();

        panel.SetActive(false);
        if (info.GetLocked() == false)
        {
            blocker.SetActive(false);
            indicator.GetComponent<MeshRenderer>().material = unlocked;
            bar.color = unlocked.color;

            Button barButton = bar.gameObject.GetComponent<Button>();

            barButton.onClick.AddListener(() =>
            {
                SwitchScene();
            });
        }
            
        RatingChecker();
    }

    private void RatingChecker()
    {
        int rating = levelInfo.GetRating();

        for (int i = 0; i < rating; i++)
        {
            //stars[i].GetComponent<MeshRenderer>().material = successStar;
            stars[i].SetActive(true);
        }
    }

    public void SwitchScene()
    {
        LevelManager.instance.activeLevel = levelInfo;
        SceneManager.LoadScene(levelInfo.GetSceneID());
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            panel.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            panel.SetActive(false);
        }
    }
}
