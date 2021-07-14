using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool firstCollisionOccured;
    public bool hitted;
    public bool shoot;
    public bool disabled;
    [SerializeField] private Sprite redSprite;
    [SerializeField] private Sprite greenSprite;

    private void Start()
    {
        firstCollisionOccured = false;
        hitted = false;
        shoot = false;
        disabled = false;
        Invoke(nameof(ChangeLayer), 0.3f);
    }

    private void Update()
    {
        if (disabled)
        {
            transform.position = new Vector3(-3, -6, 0);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy") && !hitted)
        {
            
        }
        
        if (other.gameObject.tag.Equals("Border") || other.gameObject.tag.Equals("Enemy"))
        {
            GetComponent<SpriteRenderer>().sprite = redSprite;
            hitted = true;
        }
        
        else if (other.gameObject.name.Equals("Player"))
        {
            Debug.Log("yes");
            BulletManager.Instance.bullets.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public void ChangeLayer()
    {
        if(gameObject.layer != 8)
        {
            gameObject.layer = 8;
        }
    }

    public void ReuseBullet()
    {
        GetComponent<SpriteRenderer>().sprite = greenSprite;
        hitted = false;
    }
}
