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
    }
    public void DisplayObject()
    {
        if (!itemInfo.collected)
            gameObject.transform.GetChild(1).GetComponent<Image>().sprite = itemInfo.lockedImage.sprite; 
        else
            gameObject.transform.GetChild(1).GetComponent<Image>().sprite = itemInfo.unlockedImage.sprite;
    }
    public void HighlightObject(bool isHighlight)
    {
        if (isHighlight)
            HighLightText(true);
        else
            HighLightText(false);
    }
    private void HighLightText(bool isHighlight)
    {
        if (isHighlight)
        {
            menuTitle.text = itemInfo.itemName;
            menuDesc.text = itemInfo.itemDesc;
        }
        else
        {
            menuTitle.text = "";
            menuDesc.text = "";
        }
    }
}
