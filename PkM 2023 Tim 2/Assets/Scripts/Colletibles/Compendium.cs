using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Compendium : UIList
{
    [SerializeField] private TextMeshProUGUI menuTitle;
    [SerializeField] private TextMeshProUGUI menuDesc;

    [SerializeField] private GameObject LeftPage;
    private List<CollectibleDisplayer> items = new List<CollectibleDisplayer>();

    private CollectibleManager manager;

    private void Start()
    {
        if (manager == null)
            manager = GameObject.Find("Collectible Manager").GetComponent<CollectibleManager>();

        InitializeCollectible(manager.GetAllCollectibles());
    }
    private void InitializeCollectible(List<Collectibles> collectibles)
    {
        int j = 0;
        for (int i = 0; i < collectibles.Count; i++)
        {
            if (i % 6 == 0 || i == 0)
            {
                GameObject NewPage = Instantiate(pagePrefab, LeftPage.transform);
                NewPage.name = "Page " + (j + 1);
                Pages.Add(NewPage);

                j++;
            }
            GameObject displayer = Instantiate(itemPrefab, Pages[j - 1].transform); // Make sure to instantiate on Item List
            displayer.GetComponent<CollectibleDisplayer>().Initialize(collectibles[i], menuTitle, menuDesc);

            items.Add(displayer.GetComponent<CollectibleDisplayer>());
        }

        UpdateButton();
        UpdatePage(0);
    }
    public void UpdateLocked()
    {
        for(int i = 0; i < items.Count; i++)
        {
            items[i].UpdateLocked();
        }
    }
}
