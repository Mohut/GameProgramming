using Unity.Mathematics;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    private void Start()
    {
        Invoke(nameof(Destroy), 0.1f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            AimingShoot.Instance.PlayHittedSound();
            Instantiate(explosion, other.transform.position, quaternion.identity);
            Score.Instance.AddPoints(300, other.gameObject);
            Destroy(other.gameObject);
        }
            
    }

    void Update()
    {
        transform.localScale += new Vector3(60 * Time.deltaTime, 60 * Time.deltaTime, 0);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
