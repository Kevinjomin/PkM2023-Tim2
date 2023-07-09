using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUI : UIList
{
    private LevelManager manager;
    [SerializeField] private GameObject panel;

    private void Start()
    {
        if (manager == null)
            manager = GameObject.Find("Level Manager").GetComponent<LevelManager>();

        InitializeLevels(manager.levels);
    }

    private void InitializeLevels(List<Levels> levels)
    {
        int j = 0;
        for (int i = 0; i < levels.Count; i++)
        {
            if (i % 6 == 0 || i == 0)
            {
                Debug.Log("Create Levels");
                GameObject NewPage = Instantiate(pagePrefab, panel.transform);
                NewPage.name = "Page " + (j + 1);
                Pages.Add(NewPage);

                j++;
            }
            GameObject displayer = Instantiate(itemPrefab, Pages[j - 1].transform); // Make sure to instantiate on Item List
            displayer.GetComponent<LevelDisplayer>().Initialize(levels[i]);
        }

        UpdateButton();
        UpdatePage(0);
    }
}
