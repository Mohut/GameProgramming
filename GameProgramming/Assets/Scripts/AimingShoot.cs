using UnityEngine;
using UnityEngine.Assertions.Must;

public class AimingShoot : MonoBehaviour
{
    private Vector3 mousePosition;
    private Camera mainCamera;
    private Transform gunTransform;
    private Rigidbody2D rigidbody2D;
    private float aimingAngle;
    private bool onPlatform;
    private EnemySpawner enemySpawner;
    public bool specialShootReady;
    public static AimingShoot Instance;

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
    }

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

    public void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(!enemySpawner.start)
                GetComponent<AudioSource>().PlayOneShot(music);
            enemySpawner.start = true;
            rigidbody2D.gravityScale = 0.5f;
            
            if (BulletManager.Instance.bullets.Count < BulletManager.Instance.maxBullets)
            { 
                GetComponent<AudioSource>().PlayOneShot(shootSound);
                var newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                Vector2 shotDirection = -(mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                rigidbody2D.AddForce(shotDirection.normalized * force, ForceMode2D.Impulse);
                newBullet.GetComponent<Rigidbody2D>().AddForce(-shotDirection.normalized * 25, ForceMode2D.Impulse);
                BulletManager.Instance.bullets.Add(newBullet);
            }
        }
        
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

}
