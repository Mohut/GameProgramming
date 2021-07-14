using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance { get; private set; }
    
    public List<GameObject> bullets;
    public List<GameObject> UIBullets;
    [SerializeField] private GameObject UIBullet;
    [SerializeField] private GameObject UIBulletHolder;
    public int maxBullets;
    
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
    }

    private void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            var bulletUI = Instantiate(UIBullet, transform.position, Quaternion.identity);
            bulletUI.transform.parent = UIBulletHolder.transform;
            UIBullets.Add(bulletUI);
        }

        UIBullets.Reverse();
        bullets = new List<GameObject>();
        maxBullets = 10;
    }


    private void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        for (int i = 0; i < bullets.Count; i++)
        {
            var UIElement = UIBullets[i].GetComponentsInChildren<RawImage>();
            UIElement[1].enabled = false;
        }

        for (int i = maxBullets-1; i > bullets.Count-1; i--)
        {
            var UIElement = UIBullets[i].GetComponentsInChildren<RawImage>();
            UIElement[1].enabled = true;
        }
    }
}
