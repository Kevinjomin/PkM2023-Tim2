using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<Spawner> spawner = new List<Spawner>();

    public List<SpawnData> collectedObject = new List<SpawnData>();

    private void Start()
    {
        InitializeManager();
    }
 
    private void InitializeManager()
    {
        for (int i = 0; i < spawner.Count; i++)
        {
            spawner[i].InitializeSpawner(this);
        }
    }
    public void UpdateCompendium()
    {
        for (int i = 0; i < collectedObject.Count; i++)
        {
            CollectibleManager.instance.SetByID(collectedObject[i].id);
        }
    }
}
