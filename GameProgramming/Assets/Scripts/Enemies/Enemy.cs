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
        // The enemies spawn tiny and grow for 1.5 seconds. Then the collider will be enabled
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
        if (other.gameObject.tag.Equals("Player") && DiscManager.Instance.maxDiscs > 1 && Player.Instance.hittable)
        {

            // Lets the player blind and be invincible
            Player.Instance.hittable = false;
            Player.Instance.blinkingBool = true;
            
            // The player loses 3 maximum discs
            var UIElement1 = DiscManager.Instance.UIDiscs[DiscManager.Instance.UIDiscs.Count - 1];
            DiscManager.Instance.UIDiscs.Remove(UIElement1);
            Destroy(UIElement1.gameObject);
            
            var UIElement2 = DiscManager.Instance.UIDiscs[DiscManager.Instance.UIDiscs.Count - 1];
            DiscManager.Instance.UIDiscs.Remove(UIElement2);
            Destroy(UIElement2.gameObject);
            
            var UIElement3 = DiscManager.Instance.UIDiscs[DiscManager.Instance.UIDiscs.Count - 1];
            DiscManager.Instance.UIDiscs.Remove(UIElement3);
            Destroy(UIElement3.gameObject);

            // Instantiated discs that should be removed, will be removed
            while (DiscManager.Instance.UIDiscs.Count < DiscManager.Instance.discs.Count)
            {
                var bullet = DiscManager.Instance.discs[DiscManager.Instance.discs.Count - 1];
                DiscManager.Instance.discs.Remove(bullet);
                Destroy(bullet.gameObject);
            }
            
            Player.Instance.PLayGotHitSound();
            DiscManager.Instance.maxDiscs -= 3;
        }
    }
}
