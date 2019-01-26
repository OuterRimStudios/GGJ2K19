using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public ObjectPooling pool;
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
        if (!spawning)
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
            GameObject obj = pool.GetPooledObject();

            if (obj == null) yield break;
            obj.transform.position = spawnLocation[i].position;
            obj.transform.rotation = spawnLocation[i].rotation;

            Transform child = obj.transform.GetChild(0);

            Quaternion rot = Random.rotation;
            obj.transform.Rotate(Vector3.up * rot.y);

            float randomScale = Random.Range(1, 2.25f);
            obj.transform.localScale = new Vector3(randomScale, randomScale, randomScale);

            obj.SetActive(true);
        }
        yield return new WaitForSecondsRealtime(spawnInterval);
        spawning = false;
    }
}
