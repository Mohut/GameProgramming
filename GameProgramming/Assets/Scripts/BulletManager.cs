using UnityEngine;
using UnityEngine.UI;


public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance { get; private set; }

    public int hitCounter;
    public RawImage[] backgrounds;
    public RawImage[] bulletImages;
    [SerializeField] public Bullet[] bullets;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        hitCounter = 0;
    }
    

    private void Update()
    {
        transform.position = AimingShoot.Instance.transform.position;
        UpdateBulletUI();
        SortArray();
    }

    public void SortArray()
    {
        for (int i = 0; i < bullets.Length-1; i++)
        {

            if (!bullets[i].shoot && bullets[i + 1].shoot)
            {
                Bullet bullet = bullets[i];
                bullets[i] = bullets[i + 1];
                bullets[i + 1] = bullet;
            }
        }

        for (int i = 0; i < bullets.Length - 1; i++)
        {
            if (!bullets[i].disabled && bullets[i + 1].disabled)
            {
                Bullet bullet = bullets[i];
                bullets[i] = bullets[i + 1];
                bullets[i + 1] = bullet;
            }
        }
    }
    
    public void UpdateBulletUI()
    {
        switch (Instance.hitCounter)
        {
            case 1:
                backgrounds[0].enabled = false;
                backgrounds[1].enabled = false;
                backgrounds[2].enabled = false;
                break;
            case 2:
                backgrounds[3].enabled = false;
                backgrounds[4].enabled = false;
                backgrounds[5].enabled = false;
                break;
            case 3:
                backgrounds[6].enabled = false;
                backgrounds[7].enabled = false;
                backgrounds[8].enabled = false;
                break;
        }
        
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[9-i].shoot && !bullets[9-i].disabled)
            {
                bulletImages[9-i].enabled = true;
            }
            else
            {
                bulletImages[9-i].enabled = false;
            }
        }
    }
}
