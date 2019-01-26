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

    public static bool CanSpawn;
    bool spawning;

    private void Start()
    {
        CanSpawn = true;
    }

    private void Update()
    {
        if (!CanSpawn) return;
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
        {
            GameObject enemy = Instantiate(car, spawnLocation[i].position, spawnLocation[i].rotation);
            OutOfControlManager.Instance.AddEnemy(enemy.transform);
        }
        yield return new WaitForSecondsRealtime(spawnInterval);
        spawning = false;
    }
}
