using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CollectibleManager : MonoBehaviour
{
    public List<Collectibles> collectibles = new List<Collectibles>();
    public static CollectibleManager instance;
    [SerializeField] private Compendium compendium;

    private Scene scene;

    private void Awake()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Make Sure there's only one listener
        SceneManager.sceneLoaded += OnSceneLoaded;

        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        scene = SceneManager.GetActiveScene();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!string.Equals(scene.path, this.scene.path))
            return;

        compendium = GameObject.Find("Compendium").GetComponent<Compendium>();
        DisplayCollectibles();
    }
    private void DisplayCollectibles()
    {
        compendium.InitializeCollectible(collectibles);
    }
}
