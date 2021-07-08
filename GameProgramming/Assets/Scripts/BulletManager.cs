using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance { get; private set; }

    public RawImage[] bulletImages;
    public List<Bullet> bullets;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        bullets = new List<Bullet>();
        bulletImages = GameObject.Find("Bullets").GetComponentsInChildren<RawImage>();
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
