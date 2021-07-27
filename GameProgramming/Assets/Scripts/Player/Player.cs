using System.Collections;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Player : MonoBehaviour
{
    private Vector3 mousePosition;
    private Camera mainCamera;
    private Transform gunTransform;
    private Rigidbody2D rigidbody2D;
    private float aimingAngle;
    private bool onPlatform;
    private EnemySpawner enemySpawner;
    public bool specialShootReady;
    public static Player Instance;
    public bool hittable;
    private float hitTimer;
    private float hitTime;
    public bool blinkingBool;

    [SerializeField] private GameObject shockwave;
    [SerializeField] private int force;
    [SerializeField] private GameObject bullet;
    [SerializeField] private AudioClip specialSound;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip gotHitSound;
    [SerializeField] private AudioClip hittedSound;
    [SerializeField] private AudioClip music;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        blinkingBool = false;
        hitTime = 1f;
        hitTimer = 0;
        hittable = true;
        specialShootReady = false;
        mainCamera = Camera.main;
        gunTransform = transform.Find("Weapon");
        rigidbody2D = GetComponent<Rigidbody2D>();
        aimingAngle = 0;
        onPlatform = false;
        enemySpawner = EnemySpawner.Instance;
    }

    void Update()
    {
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        MoveSprites();
        
        Aim();
        
        Shoot();

        // Lets the player blink and be invincible for some time after being hit by an enemy
        if (!hittable)
        {
            hitTimer += Time.deltaTime;
            if(hitTimer > hitTime)
                hittable = true;
        }

        if (blinkingBool)
        {
            StartCoroutine(nameof(blinking));
            blinkingBool = false;
        }
    }

    // Rotates the player sprite to face the mouse cursor
    public void MoveSprites()
    {
        if (mousePosition.x < transform.position.x)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
            gunTransform.localRotation = new Quaternion(180, 0, 0, 0);
        }
        else
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
            gunTransform.localRotation = new Quaternion(0, 0, 0, 0);
        }
    }

    // Rotates the gun to the mouse position
    public void Aim()
    {
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        aimingAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        if (mousePosition.x > transform.position.x)
        {
           gunTransform.eulerAngles = new Vector3(0, 0, aimingAngle); 
        }
        else
        {
            gunTransform.eulerAngles = new Vector3(180, 0, -aimingAngle);
        }
    }

    /*
     Instantiates a disc if the player has one to shoot after clicking the left mouse button
     and gives it force into the mouse direction. The player gets pushed in the opposite direction
    */
    public void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(!enemySpawner.start)
                GetComponent<AudioSource>().PlayOneShot(music);
            enemySpawner.start = true;
            rigidbody2D.gravityScale = 0.5f;
            
            if (DiscManager.Instance.discs.Count < DiscManager.Instance.maxDiscs)
            { 
                GetComponent<AudioSource>().PlayOneShot(shootSound);
                var newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                Vector2 shotDirection = -(mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                rigidbody2D.AddForce(shotDirection.normalized * force, ForceMode2D.Impulse);
                newBullet.GetComponent<Rigidbody2D>().AddForce(-shotDirection.normalized * 25, ForceMode2D.Impulse);
                DiscManager.Instance.discs.Add(newBullet);
            }
        }
        
        // Instantiates the special ability
        if (Input.GetMouseButtonDown(1) && specialShootReady)
        {
            GetComponent<AudioSource>().PlayOneShot(specialSound);
            Instantiate(shockwave, transform.position, Quaternion.identity);
            specialShootReady = false;
            BorderManager.Instance.ResetAllBorders();
        }
    }

    public void PlayCollectSound()
    {
        GetComponent<AudioSource>().PlayOneShot(collectSound);
    }

    public void PLayGotHitSound()
    {
        GetComponent<AudioSource>().PlayOneShot(gotHitSound);
    }

    public void PlayHittedSound()
    {
        GetComponent<AudioSource>().PlayOneShot(hittedSound);
    }
    
    // Lets the player blink
    IEnumerator blinking()
    {
        for (int i = 0; i < 5; i++)
        {
           GetComponent<SpriteRenderer>().enabled = false;
           yield return new WaitForSeconds(0.1f);
           GetComponent<SpriteRenderer>().enabled = true;
           yield return new WaitForSeconds(0.1f); 
        }
    }

}
