using UnityEngine;

public class Border : MonoBehaviour
{

    public bool activated;

    private void Start()
    {
        activated = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            GetComponent<SpriteRenderer>().color = Color.green;
            activated = true;
            BorderManager.Instance.CheckBorders();
        }
    }
}
