using System;
using UnityEngine;

public class Border : MonoBehaviour
{

    public bool activated;
    [SerializeField] private Sprite notActivatedBorder;
    [SerializeField] private Sprite activatedBorder;


    private void Start()
    {
        activated = false;
        GetComponent<AudioSource>().volume = 0.3f;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Bounce();
        }
    }

    public void ResetBorder()
    {
        GetComponent<SpriteRenderer>().sprite = notActivatedBorder;
        GetComponent<Animator>().Play("New State");
        activated = false;
    }

    public void Bounce()
    {
        GetComponent<SpriteRenderer>().sprite = activatedBorder;
        GetComponent<Animator>().Play("BorderBounce");
        activated = true;
        BorderManager.Instance.CheckBorders();
        GetComponent<AudioSource>().Play();
    }
}
