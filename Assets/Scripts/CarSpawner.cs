using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject car;
    public List<Transform> spawnPositions;
    public int minSpawn;
    public int maxSpawn;
    public float spawnInterval;

    bool spawning;

    private void Update()
    {
        if(!spawning)
        {
            spawning = true;
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        int amountToSpawn = Random.Range(minSpawn, maxSpawn + 1);
        List<Transform> spawnLocation = Utilities.GetRandomItems(spawnPositions, amountToSpawn);

        for (int i = 0; i < amountToSpawn; i++)
            Instantiate(car, spawnLocation[i].position, spawnLocation[i].rotation);
        yield return new WaitForSeconds(spawnInterval);
        spawning = false;
    }
}
