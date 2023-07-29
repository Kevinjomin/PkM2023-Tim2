using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int oneStar;
    [SerializeField] private int twoStar;
    [SerializeField] private int threeStar;

    public float timeLimit;
    public int trashToCollect;

    [Header("DONT USE THIS, ONLY FOR TESTING")]
    public int score;

    [SerializeField] private SpawnManager manager;
    [SerializeField] private ScoreUI ui;
    [SerializeField] private RatingUI ratingUi;

    private enum EndRating
    {
        FAILURE,
        ONE_STAR,
        TWO_STAR,
        THREE_STAR
    }
    [SerializeField] EndRating endRating;

    private void Start()
    {
        trashToCollect = manager.worldTrashLimit;
        ui.InitializeUI(oneStar, twoStar, threeStar);

        ui.UpdateLimit(trashToCollect);
        ui.UpdateScore(score);
    }
    public void AddScore()
    {
        score++;
        trashToCollect--;

        ui.UpdateLimit(trashToCollect);
        ui.UpdateScore(score);
    }
    public void addWrong()
    {
        trashToCollect--;

        ui.UpdateLimit(trashToCollect);
        ui.UpdateScore(score);
    }

    private string RatingDescription()
    {
        switch(endRating)
        {
            case EndRating.FAILURE:
                return "You've failed";
            case EndRating.ONE_STAR:
                return "Nice Job";
            case EndRating.TWO_STAR:
                return "Great Work";
            case EndRating.THREE_STAR:
                return "Outstanding Performance";
            default:
                return "You've finished this level";
        }
    }
    private int CheckStar()
    {
        if (score >= oneStar)
        {
            endRating = EndRating.ONE_STAR;
            if (score >= twoStar)
            {
                endRating = EndRating.TWO_STAR;
                if (score >= threeStar)
                    endRating = EndRating.THREE_STAR;
            }
        }
        else
            endRating = EndRating.FAILURE;

        return (int)endRating;
    }

    public void EndGame()
    {
        int rating = CheckStar();
        string ratingDescription = RatingDescription();

        ratingUi.DisplayRating(ratingDescription, rating);

        if (rating > 0) // If Win
        {
            manager.UpdateCompendium();
            ratingUi.WinCondition(true, LevelManager.instance.activeLevel, score, rating);
        }
        else
        {
            LevelManager.instance.LevelFailed();
        }
    }
}
