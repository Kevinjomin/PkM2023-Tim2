using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreCounter;
    [SerializeField] private TMP_Text limitCounter;
    [SerializeField] private TMP_Text timeCounter;

    [SerializeField] private TMP_Text oneStarText;
    [SerializeField] private TMP_Text twoStarText;
    [SerializeField] private TMP_Text threeStarText;

    [SerializeField] private TMP_Text WinText;
    //[SerializeField] private TMP_Text accuracyCounter;

    private void Update()
    {
        /*if (scoreSystem != null && scoreCounter != null)
        {
            scoreCounter.text = "Score: " + scoreSystem.score.ToString();
        }*/
    }

    public void InitializeUI(int one, int two, int three)
    {
        oneStarText.text = "1 Star = " + one + " Trash";
        twoStarText.text = "2 Star = " + two + " Trash";
        threeStarText.text = "3 Star =" + three + " Trash";
    }
    public void UpdateLimit(int limit)
    {
        limitCounter.text = "Trash Limit : " + limit + " Trash Left";
    }
    public void UpdateScore(int score)
    {
        scoreCounter.text = "SCORE: " + score;
    }
    public void GameEnd(string ratingText)
    {
        WinText.text = ratingText;
    }
}
