using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnData
{
    public int id;
    public int rarityInt;
    public GameObject spawnObject;
    public SpawnManager manager;
    public enum Rarity
    {
        COMMON,
        UNCOMMON,
        RARE
    }
    public Rarity rarity;

    public SpawnData(int id, int rarityInt, GameObject spawnObject, SpawnManager manager)
    {
        this.id = id;
        this.rarityInt = rarityInt;
        this.spawnObject = spawnObject;
        rarity = (Rarity)rarityInt;
        this.manager = manager;
    }
    public void UpdateCollected()
    {
        manager.collectedObject.Add(this);
    }
}
