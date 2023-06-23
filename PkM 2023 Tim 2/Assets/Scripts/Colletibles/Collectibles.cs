using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Collectibles
{
    public int id;
    public string itemName;
    public string itemDesc;
    public bool collected;
    public Image lockedImage;
    public Image unlockedImage;
}
