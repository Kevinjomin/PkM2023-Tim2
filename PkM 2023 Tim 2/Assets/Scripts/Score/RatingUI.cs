using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RatingUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ratingDescription;
    [SerializeField] private Button menuButton;
    [SerializeField] private List<GameObject> Stars = new List<GameObject>();

    private void Start()
    {
        menuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.scene.MainMenuScene);
        });
    }
    public void DisplayRating(string description, int star)
    {
        ratingDescription.text = description;
        for (int i = 0; i < star; i++)
        {
            Stars[i].SetActive(true);
        }
    }
}
