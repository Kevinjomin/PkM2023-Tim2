using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private List<Spawner> spawners = new List<Spawner>();
    [SerializeField] private float timeToSpawn;

    public int worldTrashLimit;
    public List<SpawnData> collectedObject = new List<SpawnData>();

    private void Start()
    {
        StartCoroutine(SpawnCooldown(1f));
    }
    public void AddSpawner(Spawner spawner)
    {
        spawners.Add(spawner);
    }
    private void RandomnizeSpawn()
    {
        if (worldTrashLimit <= 0)
            return;

        List<Spawner> readySpawner = new List<Spawner>();
        for (int i = 0; i < spawners.Count; i++)
        {
            if (spawners[i].readyToSpawn == true)
                readySpawner.Add(spawners[i]);
        }
        if (readySpawner.Count == 0)
        {
            StartCoroutine(SpawnCooldown(1f));
            return;
        }

        int whereToSpawn = Random.Range(0, readySpawner.Count);
        readySpawner[whereToSpawn].ChooseObject();
    }
    public void AfterSpawn()
    {
        worldTrashLimit--;
        StartCoroutine(SpawnCooldown(timeToSpawn));
    }
    private IEnumerator SpawnCooldown(float spawnTime)
    {
        yield return new WaitForSeconds(timeToSpawn);
        RandomnizeSpawn();
    }
    public void UpdateCompendium()
    {
        for (int i = 0; i < collectedObject.Count; i++)
        {
            CollectibleManager.instance.SetByID(collectedObject[i].id);
        }
    }
}
