using UnityEngine;

public class Bullet : MonoBehaviour
{
    private BulletManager bulletManager;
    private bool firstCollisionOccured;
    public bool hitted;
    [SerializeField] private Sprite redSprite;

    private void Start()
    {
        bulletManager = BulletManager.Instance;
        firstCollisionOccured = false;
        hitted = false;
        
        Invoke(nameof(ChangeLayer), 0.3f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy") && !hitted)
        {
            Destroy(other.gameObject);
            Score.Instance.AddPoints(100);
        }
            
        
        if (other.gameObject.tag.Equals("Border") || other.gameObject.tag.Equals("Enemy"))
        {
            GetComponent<SpriteRenderer>().sprite = redSprite;
            hitted = true;
        }

        if (!firstCollisionOccured)
        {
            firstCollisionOccured = true;
        }
        else if (other.gameObject.name.Equals("Player"))
        {
            bulletManager.bullets.Remove(this);
            Destroy(gameObject);
            bulletManager.UpdateBulletUI();
        }
    }

    public void ChangeLayer()
    {
        {
            gameObject.layer = 8;
        }
    }
}
