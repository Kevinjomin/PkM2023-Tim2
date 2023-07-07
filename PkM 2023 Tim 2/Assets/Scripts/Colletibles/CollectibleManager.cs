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

    private Scene scene;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        if (instance == this)
        {
            DontDestroyOnLoad(gameObject);

            scene = SceneManager.GetActiveScene();
        }
    }

    public Collectibles GetByID(int id)
    {
        for (int i = 0; i < collectibles.Count; i++)
        {
            if (id == collectibles[i].id)
            {
               return collectibles[i];
            }
        }
        return null;
    }
    public void SetByID(int id)
    {
        for (int i = 0; i < collectibles.Count; i++)
        {
            if (id == collectibles[i].id)
            {
                collectibles[i].collected = true;
            }
        }
    }
}
