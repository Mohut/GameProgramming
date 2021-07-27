using Unity.Mathematics;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    private void Start()
    {
        Invoke(nameof(Destroy), 0.15f);
    }

    // If the explosion hits an enemy the enemy will be destroyed and the player gets points
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Player.Instance.PlayHittedSound();
            Instantiate(explosion, other.transform.position, quaternion.identity);
            Score.Instance.AddPoints(300, other.gameObject);
            Destroy(other.gameObject);
        }
            
    }

    // The explosion grows and then disappears
    void Update()
    {
        transform.localScale += new Vector3(60 * Time.deltaTime, 60 * Time.deltaTime, 0);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
