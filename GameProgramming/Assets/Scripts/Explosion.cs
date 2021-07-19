using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Destroy), 0.35f);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
