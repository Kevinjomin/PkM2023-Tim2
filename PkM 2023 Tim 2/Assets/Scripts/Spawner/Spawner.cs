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
    [SerializeField] private int uncommonPercentage;
    [SerializeField] private int rarePercentage;

    [Header("DONT USE THIS, ONLY FOR TESTING")]
    [SerializeField] private List<SpawnData> commonObject = new List<SpawnData>();
    [SerializeField] private List<SpawnData> uncommonObject = new List<SpawnData>();
    [SerializeField] private List<SpawnData> rareObject = new List<SpawnData>();

    public void InitializeSpawner(SpawnManager manager)
    {
        for (int i = 0; i < idSelected.Count; i++)
        {
            Collectibles selectedCollectible = CollectibleManager.instance.GetByID(idSelected[i]);
            SpawnData mapObject = new SpawnData(selectedCollectible.id, (int)selectedCollectible.rarity, selectedCollectible.mapObject, manager);

            SortItem(mapObject);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // TESTING PURPOSES
            ChooseObject();
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
    public IEnumerator SpawnTimer() // To spawn, this should run first
    {
        yield return new WaitForSeconds(spawnCooldown);
        ChooseObject();
    }
    public void SpawnObject(SpawnData chosenObject) // Public for testing
    {
        if (spawnLimit <= 0)
            return;

        GameObject newObject =  Instantiate(chosenObject.spawnObject, this.transform);
        newObject.GetComponent<SpawnObject>().data = chosenObject;
        Debug.Log("Spawning " + newObject.GetComponent<SpawnObject>().data.rarity + " Object");

        spawnLimit--;
    }
    public void ChooseObject() // Public for testing
    {
        int chance = Random.Range(0, 100);
        if (chance <= uncommonPercentage)
        {
            if (chance <= rarePercentage)
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

        int chance = Random.Range(0, list.Count - 1);
        SpawnObject(list[chance]);
    }
}
