using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private float spawnCooldown;

    [Header("Make sure spawnChance and boxList have same length")]
    public GameObject[] boxList;
    [SerializeField] [Range(0, 1)] public float[] spawnChance; //spawn chance for each box


    private void Start()
    {
        StartCoroutine(SpawnBoxCooldown());
    }
    private IEnumerator SpawnBoxCooldown()
    {
        int randomIndex = getRandomBoxIndex();
        if(randomIndex >= 0)
        {
            Instantiate(boxList[randomIndex], transform.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(spawnCooldown);
        StartCoroutine(SpawnBoxCooldown());
    }

    private int getRandomBoxIndex()
    {
        float totalChance = 0f;
        foreach(float chance in spawnChance)
        {
            totalChance += chance;
        }

        float random = Random.Range(0f, totalChance);
        float cumulativeChance = 0f;
        for(int i=0; i<spawnChance.Length; i++)
        {
            cumulativeChance += spawnChance[i];
            if(random <= cumulativeChance)
            {
                return i;
            }
        }
        return -1; //if no valid index
    }
}
