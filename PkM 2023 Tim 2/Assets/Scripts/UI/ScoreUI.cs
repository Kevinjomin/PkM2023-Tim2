using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField]private TMP_Text scoreCounter;
    [SerializeField] private TMP_Text accuracyCounter;
    private ScoreSystem scoreSystem;

    private void Start()
    {
        scoreSystem = FindObjectOfType<ScoreSystem>();
    }

    private void Update()
    {
        if (scoreSystem != null && scoreCounter != null)
        {
            scoreCounter.text = "Score: " + scoreSystem.score.ToString();
        }
    }
}
