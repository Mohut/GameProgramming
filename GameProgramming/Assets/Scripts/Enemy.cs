using System;
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
    
}
