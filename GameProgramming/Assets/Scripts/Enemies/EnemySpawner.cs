using System;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    [SerializeField] private GameObject enemy;
    public bool start;
    private float spawnTime;
    private float spawnTimer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        start = false;
        spawnTime = 3;
        spawnTimer = 0;
        InvokeRepeating(nameof(ReduceSpawnTime), 10, 10);
    }

    private void Update()
    {
        // Spawns enemies continuously faster
        spawnTimer += Time.deltaTime;
        if (spawnTime < spawnTimer)
        {
            SpawnEnemy();
            spawnTimer = 0;
        }
    }

    // Spawns enemy at a random x and y coordination
    public void SpawnEnemy()
    {
        if (start)
        {
            var randomX = Random.Range(-7f, 7f);
            var randomY = Random.Range(-3f, 3f);

            Instantiate(enemy, new Vector3(randomX, randomY, 0), Quaternion.identity);
        }
    }

    // Reduces the spawnTime so let enemies spawn continuously faster
    public void ReduceSpawnTime()
    {
        spawnTime -= 0.5f;
    }
}
