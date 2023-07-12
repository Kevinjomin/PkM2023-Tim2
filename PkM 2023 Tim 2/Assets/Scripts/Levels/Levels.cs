using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Levels
{
    [SerializeField] private string levelName;
    public enum Rating
    {
        NONE,
        ONE_STAR,
        TWO_STAR,
        THREE_STAR
    }
    [SerializeField] private Rating rating;
    [SerializeField] private int score;

    [SerializeField] private bool locked = true;

    [SerializeField] private int nextLevelID;  // Can be List but singular for now
    [SerializeField] private int sceneID;
    private Levels nextLevel;

    public void InitializeLevel(Levels nextLevel)
    {
        this.nextLevel = nextLevel;
    }

    public string GetLevelName()
    {
        return levelName;
    }
    public int GetSceneID()
    {
        return sceneID;
    }
    public bool GetLocked()
    {
        return locked;
    }

    public int GetNextLevelID()
    {
        return nextLevelID;
    }
    public Levels GetNextLevel()
    {
        return nextLevel;
    }

    public void UnlockThisLevel()
    {
        locked = false;
    }
    public void LevelCompleted(int rating, int score)
    {
        if (rating > (int)this.rating)
            this.rating = (Rating)rating;

        if (score > this.score)
            this.score = score;

        nextLevel.UnlockThisLevel();
    }

    public int GetRating()
    {
        return (int)rating; 
    }
    public int GetScore()
    {
        return score;
    }
}
