using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnData
{
    public int id;
    public string name;
    public GameObject spawnObject;
    public SpawnManager manager;

    public enum Rarity
    {
        COMMON,
        UNCOMMON,
        RARE
    }
    public Rarity rarity;

    [Header("USED TO INPUT RARITY ENUM")]
    public int rarityInt;

    public SpawnData(int id, string name, int rarityInt, GameObject spawnObject, SpawnManager manager)
    {
        this.id = id;
        this.name = name;
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
