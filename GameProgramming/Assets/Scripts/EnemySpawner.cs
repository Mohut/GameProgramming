using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;
    [SerializeField] private GameObject enemy;
    public bool start;

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

        start = false;
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1, 3);
    }

    public void SpawnEnemy()
    {
        if (start)
        {
            var randomX = Random.Range(-7f, 7f);
            var randomY = Random.Range(-3f, 3f);

            Instantiate(enemy, new Vector3(randomX, randomY, 0), Quaternion.identity);
        }
    }
       
    
}
