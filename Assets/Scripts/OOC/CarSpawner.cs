using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSpawner : MonoBehaviour
{
    public static CarSpawner Instance;
    public GameObject car;
    public List<Transform> spawnPositions;
    public int minSpawn;
    public int maxSpawn;
    public float spawnInterval;

    public bool CanSpawn;
    bool spawning;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(5.5f);
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
