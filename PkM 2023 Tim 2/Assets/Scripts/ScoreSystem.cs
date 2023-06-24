using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] public int score;
    [SerializeField] private int trashCount;
    [SerializeField] private int wrongCount;

    private void Awake()
    {
        score = 0;
        trashCount = 0;
        wrongCount = 0;
    }

    public void addScore(int scoreAdded)
    {
        score += scoreAdded;
        trashCount++;
    }

    public void addWrong()
    {
        trashCount++;
        wrongCount++;
    }
}
