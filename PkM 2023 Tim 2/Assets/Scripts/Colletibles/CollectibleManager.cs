using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CollectibleManager : MonoBehaviour
{
    [SerializeField] private List<Collectibles> collectibles = new List<Collectibles>();
    public static CollectibleManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        if (instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        if (collectibles.Count <= 0)
        {
            Debug.LogWarning("No collectible on data");
        }
    }
    public List<Collectibles> GetAllCollectibles()
    {
        return collectibles;
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
