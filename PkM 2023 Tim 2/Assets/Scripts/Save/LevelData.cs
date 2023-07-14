using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData : MonoBehaviour
{
    public int rating;
    public int score;
    public bool locked = true;

    public LevelData(int rating, int score, bool locked)
    {
        this.rating = rating;
        this.score = score;
        this.locked = locked;
    }
}
