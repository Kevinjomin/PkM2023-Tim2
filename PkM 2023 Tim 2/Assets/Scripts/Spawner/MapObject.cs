using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    public int id;
    public int rarityInt;
    public GameObject spawnObject;
    public enum Rarity
    {
        COMMON,
        UNCOMMON,
        RARE
    }
    public Rarity rarity;

    public MapObject(int id, int rarityInt, GameObject spawnObject)
    {
        this.id = id;
        this.rarityInt = rarityInt;
        this.spawnObject = spawnObject;
        rarity = (Rarity)rarityInt;
    }
}
