using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject enemy;

    private void Start()
    {
       InvokeRepeating(nameof(SpawnEnemy), 1, 4); 
    }

    public void SpawnEnemy()
    {
        
        GenerateNumbers:
            var randomX = Random.Range(-0.9f, 1.0f);
            var randomY = Random.Range(-0.9f, 1.0f);

            if (randomX < 0.5 && randomX > -0.5 && randomY < 0.5 && randomY > -0.5)
            {
                goto GenerateNumbers;
            }
        
        
        var currentEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
        currentEnemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(randomX * (10000 * Time.deltaTime),
            randomY * (10000 * Time.deltaTime)), ForceMode2D.Impulse);
    }

}
