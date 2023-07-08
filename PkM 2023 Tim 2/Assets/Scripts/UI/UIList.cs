using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIList : MonoBehaviour
{
    [SerializeField] protected GameObject itemPrefab;
    [SerializeField] protected GameObject pagePrefab;

    [SerializeField] protected List<GameObject> Pages = new List<GameObject>();

    [SerializeField] protected GameObject PreviousPage;
    [SerializeField] protected GameObject NextPage;

    protected int pageActive = 0;

    protected void UpdateButton()
    {
        PreviousPage.SetActive(true);
        NextPage.SetActive(true);

        if (pageActive == 0)
            PreviousPage.SetActive(false);
        if (pageActive == Pages.Count - 1)
            NextPage.SetActive(false);
    }
    protected void UpdatePage(int PageActive)
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
