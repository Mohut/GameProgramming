using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool firstCollisionOccured;
    public bool hitted;
    public bool disabled;
    [SerializeField] private Sprite redSprite;
    private int rotateCounter;
    private float rotateTimer;
    private float rotateTime;
    [SerializeField] private GameObject explosion;

    private void Start()
    {
        firstCollisionOccured = false;
        hitted = false;
        disabled = false;
        Invoke(nameof(ChangeLayer), 0.3f);
        rotateCounter = 0;
        rotateTimer = 0;
        rotateTime = 0.3f;
    }

    private void Update()
    {
        if (disabled)
        {
            transform.position = new Vector3(-3, -6, 0);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

        rotateTimer += Time.deltaTime;
        if (rotateTimer > rotateTime)
        {
            Rotate();
            rotateTimer = 0;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy") && !hitted)
        {
            Instantiate(explosion, other.transform.position, quaternion.identity);
            Score.Instance.AddPoints(100, other.gameObject);
            Destroy(other.gameObject);
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

    public void Rotate()
    {
        switch (rotateCounter)
        {
            case 0:
                GetComponent<SpriteRenderer>().flipX = false;
                rotateCounter = 1;
                break;
            case 1:
                GetComponent<SpriteRenderer>().flipY = true;
                rotateCounter = 2;
                break;
            case 2:
                GetComponent<SpriteRenderer>().flipX = true;
                rotateCounter = 3;
                break;
            case 3:
                GetComponent<SpriteRenderer>().flipY = false;
                rotateCounter = 0;
                break;
        }
    }
}
