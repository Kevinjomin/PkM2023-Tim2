using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    [SerializeField] private float spawnCooldown;

    public GameObject[] boxList;

    private void Start()
    {
        StartCoroutine(SpawnBoxCooldown());
    }
    private IEnumerator SpawnBoxCooldown()
    {
        int randomIndex = Random.Range(0, boxList.Length);
        Instantiate(boxList[randomIndex], transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnCooldown);
        StartCoroutine(SpawnBoxCooldown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
