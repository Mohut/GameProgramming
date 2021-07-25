using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float growTimer;
    private float growTime;
    private BoxCollider2D collider;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        growTime = 1.5f;
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (growTime > growTimer)
        {
            growTimer += Time.deltaTime;
            transform.localScale += new Vector3(0.5f * Time.deltaTime, 0.5f * Time.deltaTime, 0);
        }
        else
        {
            collider.enabled = true;
            spriteRenderer.color = new Vector4(256, 256, 256, 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player") && BulletManager.Instance.maxBullets > 1)
        {
            var UIElement1 = BulletManager.Instance.UIBullets[BulletManager.Instance.UIBullets.Count - 1];
            BulletManager.Instance.UIBullets.Remove(UIElement1);
            Destroy(UIElement1.gameObject);
            
            var UIElement2 = BulletManager.Instance.UIBullets[BulletManager.Instance.UIBullets.Count - 1];
            BulletManager.Instance.UIBullets.Remove(UIElement2);
            Destroy(UIElement2.gameObject);
            
            var UIElement3 = BulletManager.Instance.UIBullets[BulletManager.Instance.UIBullets.Count - 1];
            BulletManager.Instance.UIBullets.Remove(UIElement3);
            Destroy(UIElement3.gameObject);

            while (BulletManager.Instance.UIBullets.Count < BulletManager.Instance.bullets.Count)
            {
                var bullet = BulletManager.Instance.bullets[BulletManager.Instance.bullets.Count - 1];
                BulletManager.Instance.bullets.Remove(bullet);
                Destroy(bullet.gameObject);
            }

            AimingShoot.Instance.PLayGotHitSound();
            BulletManager.Instance.maxBullets -= 3;
        }
    }
}
