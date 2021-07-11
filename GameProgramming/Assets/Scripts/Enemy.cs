using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float growTimer;
    private float growTime;

    private void Start()
    {
        growTime = 1.5f;
    }

    void Update()
    {
        growTimer += Time.deltaTime;
        if (growTime > growTimer)
        { 
            transform.localScale += new Vector3(0.5f * Time.deltaTime, 0.5f * Time.deltaTime, 0);  
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            BulletManager.Instance.hitCounter++;

            switch (BulletManager.Instance.hitCounter)
            {
                case 1:
                    BulletManager.Instance.bullets[0].disabled = true;
                    BulletManager.Instance.bullets[1].disabled = true;
                    BulletManager.Instance.bullets[2].disabled = true;
                    break;
                case 2:
                    BulletManager.Instance.bullets[3].disabled = true;
                    BulletManager.Instance.bullets[4].disabled = true;
                    BulletManager.Instance.bullets[5].disabled = true;
                    break;
                case 3:
                    Debug.Log("yes");
                    BulletManager.Instance.bullets[6].disabled = true;
                    BulletManager.Instance.bullets[7].disabled = true;
                    BulletManager.Instance.bullets[8].disabled = true;
                    break;
            }
        }
    }
}
