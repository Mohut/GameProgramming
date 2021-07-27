using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Lets the explosion animation disappear
    void Start()
    {
        Invoke(nameof(Destroy), 0.35f);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
