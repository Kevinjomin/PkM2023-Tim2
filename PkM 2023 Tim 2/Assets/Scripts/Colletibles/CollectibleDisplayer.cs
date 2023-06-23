using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectibleDisplayer : MonoBehaviour
{
    [SerializeField] private Collectibles itemInfo;
    [SerializeField] private TextMeshProUGUI menuTitle;
    [SerializeField] private TextMeshProUGUI menuDesc;

    public void Initialize(Collectibles info, TextMeshProUGUI title, TextMeshProUGUI desc)
    {
        itemInfo = info;
        menuTitle = title;
        menuDesc = desc;

        if (!itemInfo.collected)
            gameObject.transform.GetChild(0).GetComponent<Image>().sprite = itemInfo.lockedImage;

        else
            gameObject.transform.GetChild(0).GetComponent<Image>().sprite = itemInfo.unlockedImage;
    }
    public void HighLightText()
    {
        if (itemInfo.collected)
        {
            menuTitle.text = itemInfo.itemName;
            menuDesc.text = itemInfo.itemDesc;
        }
        else
        {
            menuTitle.text = "???";
            menuDesc.text = "?????????";
        }
    }
}
