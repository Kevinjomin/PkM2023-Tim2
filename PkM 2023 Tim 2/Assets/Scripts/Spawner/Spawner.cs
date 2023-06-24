using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<int> idSelected = new List<int>();

    [SerializeField] private int spawnLimit;
    [SerializeField] private float radius;

    [Header("DONT USE THIS, ONLY FOR TESTING")]
    [SerializeField] private List<SpawnData> selectedMapObject = new List<SpawnData>();

    public void InitializeSpawner(SpawnManager manager)
    {
        for (int i = 0; i < idSelected.Count; i++)
        {
            Collectibles selectedCollectible = CollectibleManager.instance.GetByID(idSelected[i]);
            SpawnData mapObject = new SpawnData(selectedCollectible.id, (int)selectedCollectible.rarity, selectedCollectible.mapObject, manager);

            selectedMapObject.Add(mapObject);
        }
    }
    private void SpawnObject(SpawnData chosenObject)
    {
        Instantiate(chosenObject.spawnObject, this.transform);
        spawnLimit++;
    }
    public void ChooseObject()
    {
        float rare = 0.1f;
        float uncommon = 0.25f;

        float chance = Random.Range(0, 1);

        if (chance < uncommon)
        {
            if (chance < rare)
            {

            }
        }
        else
        {

        }
    }
}
