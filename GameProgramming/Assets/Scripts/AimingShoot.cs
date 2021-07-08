using System;
using UnityEngine;

public class AimingShoot : MonoBehaviour
{
    private Vector3 mousePosition;
    private Camera mainCamera;
    private Transform gunTransform;
    private Rigidbody2D rigidbody2D;
    private BulletManager bulletManager;
    private float aimingAngle;
    private bool onPlatform;

    [SerializeField] private int force;
    [SerializeField] private GameObject bullet;

    void Start()
    {
        mainCamera = Camera.main;
        gunTransform = transform.Find("Weapon");
        rigidbody2D = GetComponent<Rigidbody2D>();
        bulletManager = BulletManager.Instance;
        aimingAngle = 0;
        onPlatform = false;
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
            if (bulletManager.bullets.Count < 10)
            { 
                Vector2 shotVelocity = -(mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                rigidbody2D.AddForce(shotVelocity.normalized * (force * Time.deltaTime), ForceMode2D.Impulse);

                var shotBullet = Instantiate(bullet, gunTransform.position, Quaternion.identity);
                bulletManager.bullets.Add(shotBullet.GetComponent<Bullet>());
                bulletManager.UpdateBulletUI();
                
                shotBullet.GetComponent<Rigidbody2D>().
                    AddForce(-shotVelocity.normalized * (10000 * Time.deltaTime), ForceMode2D.Impulse);
            }
        }
    }

}
