using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Compendium : MonoBehaviour
{
    [SerializeField] private GameObject colletiblesPrefab;
    [SerializeField] private GameObject pagePrefab;

    [SerializeField] private TextMeshProUGUI menuTitle;
    [SerializeField] private TextMeshProUGUI menuDesc;

    [SerializeField] private GameObject LeftPage;
    [SerializeField] private List<GameObject> Pages = new List<GameObject>();

    [SerializeField] private GameObject PreviousPage;
    [SerializeField] private GameObject NextPage;

    private int pageActive = 0;
    private CollectibleManager manager;

    private void Start()
    {
        if (manager == null)
            manager = GameObject.Find("Collectible Manager").GetComponent<CollectibleManager>();
        InitializeCollectible(manager.collectibles);
    }
    public void InitializeCollectible(List<Collectibles> collectibles)
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
            GameObject displayer = Instantiate(colletiblesPrefab, Pages[j - 1].transform); // Make sure to instantiate on Item List
            displayer.GetComponent<CollectibleDisplayer>().Initialize(collectibles[i], menuTitle, menuDesc);
        }

        UpdateButton();
        UpdatePage(0);
    }
    private void UpdateButton()
    {
        PreviousPage.SetActive(true);
        NextPage.SetActive(true);

        if (pageActive == 0)
            PreviousPage.SetActive(false);
        if (pageActive == Pages.Count - 1)
            NextPage.SetActive(false);
    }
    private void UpdatePage(int PageActive)
    {
        for (int i = 0; i < Pages.Count; i++)
            Pages[i].SetActive(false);

        Pages[PageActive].SetActive(true);  
    }
    public void ChangePage(bool next)
    {
        if (next)
            pageActive += 1;
        else
            pageActive -= 1;

        UpdatePage(pageActive);
        UpdateButton();
    }
}
