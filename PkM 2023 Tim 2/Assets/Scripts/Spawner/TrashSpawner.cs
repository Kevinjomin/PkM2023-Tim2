using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [SerializeField] private List<Spawner> spawner = new List<Spawner>();

    [SerializeField] public List<int> idSelected = new List<int>();
    public List<MapObject> selectedMapObject = new List<MapObject>();

    public List<MapObject> collectedObject = new List<MapObject>();

    private void Start()
    {
        InitializeSpawner();
    }
    private void InitializeSpawner()
    {
        for (int i = 0; i < idSelected.Count; i++)
        {
            Collectibles selectedCollectible = CollectibleManager.instance.GetByID(idSelected[i]);
            MapObject mapObject = new MapObject(selectedCollectible.id, (int)selectedCollectible.rarity, selectedCollectible.mapObject, this);

            selectedMapObject.Add(mapObject);
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
