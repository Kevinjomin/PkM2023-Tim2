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
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // Make Sure there's only one listener
            SceneManager.sceneLoaded += OnSceneLoaded;

            DontDestroyOnLoad(gameObject);

            scene = SceneManager.GetActiveScene();
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Loaded a New Scene");
        if (compendium == null)
        {
            compendium = GameObject.Find("Compendium").GetComponent<Compendium>();
        }
        DisplayCollectibles();
    }
    private void DisplayCollectibles()
    {
        compendium.InitializeCollectible(collectibles);
    }

    public Collectibles FindByID(int id)
    {
        Collectibles selected = new Collectibles();
        for (int i = 0; i < collectibles.Count; i++)
        {
            if (id == collectibles[i].id)
            {
                selected = collectibles[i];
                break;
            }
        }
        return selected;
    }
}
