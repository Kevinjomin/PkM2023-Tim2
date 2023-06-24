using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public int id;
    public int rarityInt;
    public enum Rarity
    {
        COMMON,
        UNCOMMON,
        RARE
    }
    public Rarity rarity;

    public bool collected;
    public MapObject(int id, int rarityInt)
    {
        this.id = id;
        this.rarityInt = rarityInt;
        rarity = (Rarity)rarityInt;
    }
    public void SetCollected(bool collected)
    {
        this.collected = collected;
    }
}
