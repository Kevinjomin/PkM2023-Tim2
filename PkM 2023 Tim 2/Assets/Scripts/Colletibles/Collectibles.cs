using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Collectibles
{
    public enum Rarity
    {
        COMMON,
        UNCOMMON,
        RARE
    }
    public enum Type
    {
        ORGANIC,
        INORGANIC,
        ELECTRONIC
    }
    public int id;

    public string itemName;
    public string itemDesc;

    public Rarity rarity;
    public Type type;

    public Sprite lockedImage;
    public Sprite unlockedImage;

    public bool collected;

    public GameObject mapObject;
}
