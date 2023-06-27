using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int oneStar;
    [SerializeField] private int twoStar;
    [SerializeField] private int threeStar;

    public int worldTrashLimit;
    public float timeLimit;

    [Header("DONT USE THIS, ONLY FOR TESTING")]
    public int score;

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
        ui.InitializeUI(oneStar, twoStar, threeStar);

        ui.UpdateLimit(worldTrashLimit);
        ui.UpdateScore(score);
    }
    public void AddScore()
    {
        score++;
        ui.UpdateScore(score);
    }
    public void DecreaseLimit()
    {
        worldTrashLimit--;
        ui.UpdateLimit(worldTrashLimit);
    }

    public void EndGame()
    {
        int rating = CheckStar();
        string ratingDescription = RatingDescription();

        ratingUi.DisplayRating(ratingDescription, rating);
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
}
