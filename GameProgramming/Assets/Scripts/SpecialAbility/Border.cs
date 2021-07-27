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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Bounce();
        }
    }

    // Resets the borders to brown if the player activates the special attack
    public void ResetBorder()
    {
        GetComponent<SpriteRenderer>().sprite = notActivatedBorder;
        GetComponent<Animator>().Play("New State");
        activated = false;
    }

    // Animates the border if the player hits it and switches it to "activated" for the special attack
    public void Bounce()
    {
        GetComponent<SpriteRenderer>().sprite = activatedBorder;
        GetComponent<Animator>().Play("BorderBounce");
        activated = true;
        BorderManager.Instance.CheckBorders();
        GetComponent<AudioSource>().Play();
    }
}
