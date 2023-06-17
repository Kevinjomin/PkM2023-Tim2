using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button OptionsButton;
    [SerializeField] private Button HelpButton;
    [SerializeField] private Button CollectionButton;
    [SerializeField] private Button QuitButton;

    private void Awake()
    {
        // make event for every button clicks that changes the scene
        PlayButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.scene.GameScene);
        });
        QuitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
