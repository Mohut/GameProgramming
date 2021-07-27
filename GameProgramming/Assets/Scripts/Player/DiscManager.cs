using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DiscManager : MonoBehaviour
{
    public static DiscManager Instance { get; private set; }
    
    public List<GameObject> discs;
    public List<GameObject> UIDiscs;
    [SerializeField] private GameObject UIDisc;
    [SerializeField] private GameObject UIDiscHolder;
    public int maxDiscs;
    
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
        
        // Generates the upper left UI part for the ammunition system
        for (int i = 0; i < 10; i++)
        {
            var bulletUI = Instantiate(UIDisc, transform.position, Quaternion.identity);
            bulletUI.transform.parent = UIDiscHolder.transform;
            UIDiscs.Add(bulletUI);
        }

        UIDiscs.Reverse();
        discs = new List<GameObject>();
        maxDiscs = 10;
    }


    private void Update()
    {
        UpdateUI();
    }

    // Updates the UI so the player knows how many disc he can shoot
    void UpdateUI()
    {
        for (int i = 0; i < discs.Count; i++)
        {
            var UIElement = UIDiscs[i].GetComponentsInChildren<RawImage>();
            UIElement[1].enabled = false;
        }

        for (int i = maxDiscs-1; i > discs.Count-1; i--)
        {
            var UIElement = UIDiscs[i].GetComponentsInChildren<RawImage>();
            UIElement[1].enabled = true;
        }
    }
}
