using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RatingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ratingTitle;
    [SerializeField] private TextMeshProUGUI ratingDescription;

    [SerializeField] private List<GameObject> Stars = new List<GameObject>();

    public void DisplayRating(string title, string description, int star)
    {
        ratingTitle.text = title;
        ratingDescription.text = description;
        for (int i = 0; i < star; i++)
        {
            Stars[i].SetActive(true);
        }
    }
}
