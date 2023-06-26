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

    private enum EndRating
    {
        FAILURE,
        ONE_STAR,
        TWO_STAR,
        THREE_STAR
    }
    [SerializeField] EndRating endRating;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            GameEnd();
    }
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

    public void GameEnd()
    {
        CheckStar();

        string ratingText = "THERE SHOULD BE A RATING TEXT HERE";
        switch(endRating)
        {
            case EndRating.FAILURE:
                ratingText = "You've Failed";
                break;
            case EndRating.ONE_STAR:
                ratingText = "1 STAR !!!!!";
                break;
            case EndRating.TWO_STAR:
                ratingText = "2 STAR !!!!!";
                break;
            case EndRating.THREE_STAR:
                ratingText = "3 STAR !!!!!";
                break;
        }

        ui.GameEnd(ratingText);
    }
    private void CheckStar()
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
    }
}
