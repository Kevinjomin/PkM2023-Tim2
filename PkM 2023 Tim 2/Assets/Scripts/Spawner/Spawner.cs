using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    List<MapObject> ObjectPool = new List<MapObject>();
    private bool spawned; 
    private void SpawnObject(MapObject chosenObject)
    {
        Instantiate(chosenObject.spawnObject, this.transform);
        spawned = true;
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
