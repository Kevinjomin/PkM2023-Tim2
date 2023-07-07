using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<int> idSelected = new List<int>();

    [SerializeField] private int spawnLimit;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private float radius;

    [Header("Common Percentage = 100 - (Rare + Uncommon)")]
    [SerializeField] private float uncommonPercentage;
    [SerializeField] private float rarePercentage;

    [Header("DONT USE THIS, ONLY FOR TESTING")]
    [SerializeField] private List<SpawnData> commonObject = new List<SpawnData>();
    [SerializeField] private List<SpawnData> uncommonObject = new List<SpawnData>();
    [SerializeField] private List<SpawnData> rareObject = new List<SpawnData>();

    [SerializeField] private GameObject spawnParent;
    [SerializeField] private SpawnManager manager;

    [HideInInspector] public bool readyToSpawn = true;

    private void Start()
    {
        if (manager == null)
            manager = GameObject.Find("Trash Spawner").GetComponent<SpawnManager>();

        InitializeSpawner();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // TESTING PURPOSES
            ChooseObject();
    }
    //Initialize Stuff
    public void InitializeSpawner()
    {
        for (int i = 0; i < idSelected.Count; i++)
        {
            Collectibles selectedCollectible = CollectibleManager.instance.GetByID(idSelected[i]);
            SpawnData mapObject = new SpawnData(selectedCollectible.id, selectedCollectible.itemName, (int)selectedCollectible.rarity, selectedCollectible.mapObject, manager);

            SortItem(mapObject);
        }
        if (idSelected.Count <= 0 || spawnLimit <= 0)
            DeactivateSpawner();

        InitializeChances();
        manager.AddSpawner(this);
    }
    private void InitializeChances()
    {
        float commonPercentage = 100 - (uncommonPercentage + rarePercentage);
        if (commonObject.Count <= 0)
        {
            uncommonPercentage += commonPercentage / 2;
            rarePercentage += commonPercentage / 2;
            if (uncommonObject.Count <= 0)
            {
                rarePercentage += uncommonPercentage;
                uncommonPercentage = 0;
            }
                
            if (rareObject.Count <= 0)
            {
                uncommonPercentage += rarePercentage;
                rarePercentage = 0;
            }     
        }
        else
        {
            if (uncommonObject.Count <= 0)
                uncommonPercentage = 0;
            if (rareObject.Count <= 0)
                rarePercentage = 0;
        }
    }
    private void SortItem(SpawnData mapObject)
    {
        switch(mapObject.rarity)
        {
            case SpawnData.Rarity.COMMON:
                commonObject.Add(mapObject);
                break;
            case SpawnData.Rarity.UNCOMMON:
                uncommonObject.Add(mapObject);
                break;
            case SpawnData.Rarity.RARE:
                rareObject.Add(mapObject);
                break;
        }
    }
    private void DeactivateSpawner()
    {
        Debug.Log("There's no item that can be spawned in " + this.gameObject.name);
        this.gameObject.SetActive(false);
        readyToSpawn = false;
    }

    //Spawning Stuff
    private IEnumerator SpawnTimer() // To spawn, this should run first
    {
        yield return new WaitForSeconds(spawnCooldown);
        readyToSpawn = true;
    }
    public void SpawnObject(SpawnData chosenObject) // Public for testing
    {
        float randomX = Random.Range(0, radius);
        float randomZ = Random.Range(0, radius);

        GameObject newObject =  Instantiate(chosenObject.spawnObject, spawnParent.transform);
        newObject.transform.localPosition = new Vector3(randomX, 0, randomZ);
        newObject.GetComponent<SpawnObject>().data = chosenObject;

        Debug.Log("Spawning " + newObject.GetComponent<SpawnObject>().data.rarity + " Object");

        AfterSpawn();
    }
    private void AfterSpawn()
    {
        readyToSpawn = false;

        if (spawnLimit <= 0)
            return;

        StartCoroutine(SpawnTimer());
        manager.AfterSpawn();
        spawnLimit--;
    }
    public void ChooseObject() // Public for testing
    {
        float chance = Random.Range(0, 100);
        if (chance < uncommonPercentage)
        {
            if (chance < rarePercentage)
                ChooseFromList(rareObject);
            else
                ChooseFromList(uncommonObject);
        }
        else
            ChooseFromList(commonObject);
    }

    private void ChooseFromList(List<SpawnData> list)
    {
        if (list.Count == 0)
            ChooseObject();

        int chance = Random.Range(0, list.Count);
        SpawnObject(list[chance]);
    }
}
