using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<Spawner> spawner = new List<Spawner>();
    [SerializeField] private float timeToSpawn;

    public int worldTrashLimit;
    public List<SpawnData> collectedObject = new List<SpawnData>();

    private void Start()
    {
        if (spawner.Count == 0)
        {
            Debug.Log("NO SPAWNER IN THE MAP");
            return;
        }
        InitializeManager();
        StartCoroutine(SpawnCooldown(1f));
    }
    private void RandomnizeSpawn()
    {
        if (worldTrashLimit <= 0)
            return;

        List<Spawner> readySpawner = new List<Spawner>();
        for (int i = 0; i < spawner.Count; i++)
        {
            if (spawner[i].readyToSpawn == true)
                readySpawner.Add(spawner[i]);
        }
        if (readySpawner.Count == 0)
        {
            StartCoroutine(SpawnCooldown(1f));
            return;
        }

        int whereToSpawn = Random.Range(0, readySpawner.Count - 1);
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
