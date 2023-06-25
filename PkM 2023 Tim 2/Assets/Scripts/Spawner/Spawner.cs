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
    [Range(0, 100)]
    [SerializeField] private int uncommonPercentage;
    [Range(0,100)]
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
        if (spawnLimit <= 0)
            yield return null;

        yield return new WaitForSeconds(spawnCooldown);
        ChooseObject();
    }
    public void SpawnObject(SpawnData chosenObject) // Public for testing
    {
        GameObject newObject =  Instantiate(chosenObject.spawnObject, this.transform);
        newObject.GetComponent<SpawnObject>().data = chosenObject;

        spawnLimit--;
    }
    public SpawnData ChooseObject() // Public for testing
    {
        int chance = Random.Range(0, 100);
        if (chance <= uncommonPercentage)
        {
            if (chance <= rarePercentage)
                return ChooseFromList(rareObject);
            else
                return ChooseFromList(uncommonObject);
        }
        else
            return ChooseFromList(commonObject);
    }
    private SpawnData ChooseFromList(List<SpawnData> list)
    {
        if (list.Count == 0)
            ChooseObject();

        int chance = Random.Range(0, list.Count - 1);
        return list[chance];
    }
}
