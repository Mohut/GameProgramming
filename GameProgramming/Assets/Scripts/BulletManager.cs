using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance { get; private set; }

    public RawImage[] bulletImages;
    public List<Bullet> bullets;
    public bool noBulletsLeft;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        noBulletsLeft = false;
    }

    void Start()
    {
        bullets = new List<Bullet>();
        bulletImages = GameObject.Find("Bullets").GetComponentsInChildren<RawImage>();
    }

    private void Update()
    {
        if (bullets.Count == 10)
        {
            noBulletsLeft = true;
        }
        else
        {
            noBulletsLeft = false;
        }
    }

    public void UpdateBulletUI()
    {
        foreach (var image in bulletImages)
        {
            image.gameObject.SetActive(false);
        }

        for (int i = 0; i < 10 - bullets.Count; i++)
        {
            bulletImages[i].gameObject.SetActive(true);
        }
    }
}
