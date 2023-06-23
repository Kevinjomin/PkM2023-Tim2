using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Compendium : MonoBehaviour
{
    [SerializeField] private GameObject colletiblesPrefab;
    [SerializeField] private TextMeshProUGUI menuTitle;
    [SerializeField] private TextMeshProUGUI menuDesc;

    public void ShowCollectible(List<Collectibles> collectibles)
    {
        for (int i = 0; i < collectibles.Count; i++)
        {
            GameObject displayer =  Instantiate(colletiblesPrefab); // Make sure to instantiate on Item List
            displayer.GetComponent<CollectibleDisplayer>().Initialize(collectibles[i], menuTitle, menuDesc);
            displayer.GetComponent<CollectibleDisplayer>().DisplayObject();
        }
    }
}
