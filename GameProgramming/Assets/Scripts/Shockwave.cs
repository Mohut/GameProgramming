using UnityEngine;

public class Shockwave : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(Destroy), 0.1f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Score.Instance.AddPoints(300, other.gameObject);
            Destroy(other.gameObject);
        }
            
    }

    void Update()
    {
        transform.localScale += new Vector3(60 * Time.deltaTime, 60 * Time.deltaTime, 0);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
