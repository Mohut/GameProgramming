using Unity.Mathematics;
using UnityEngine;

public class Disc : MonoBehaviour
{
    private bool firstCollisionOccured;
    public bool hitted;
    [SerializeField] private Sprite redSprite;
    private int rotateCounter;
    private float rotateTimer;
    private float rotateTime;
    [SerializeField] private GameObject explosion;

    private void Start()
    {
        // If firstCollisionOccured is true the disc can't destroy enemies anymore
        firstCollisionOccured = false;
        hitted = false;
    
        Invoke(nameof(ChangeLayer), 0.3f);
        rotateCounter = 0;
        rotateTimer = 0;
        rotateTime = 0.3f;
    }

    private void Update()
    {
        // Animates the disc
        rotateTimer += Time.deltaTime;
        if (rotateTimer > rotateTime)
        {
            Rotate();
            rotateTimer = 0;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Destroys the enemy if the disc hits it
        if (other.gameObject.tag.Equals("Enemy") && !hitted)
        {
            GetComponent<AudioSource>().Play();
            Instantiate(explosion, other.transform.position, quaternion.identity);
            Score.Instance.AddPoints(100, other.gameObject);
            Destroy(other.gameObject);
        }
        
        // Changes disc color and it can't destroys enemies now
        if (other.gameObject.tag.Equals("Border") || other.gameObject.tag.Equals("Enemy"))
        {
            GetComponent<SpriteRenderer>().sprite = redSprite;
            hitted = true;
        }
        
        // Disc disappears so the player can shoot it again
        else if (other.gameObject.name.Equals("Player"))
        {
            Player.Instance.PlayCollectSound();
            DiscManager.Instance.discs.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    // Changes the layer of the disc so the player won't collect it instantly
    public void ChangeLayer()
    {
        if(gameObject.layer != 8)
        {
            gameObject.layer = 8;
        }
    }

    // Rotates the disc to look better
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
