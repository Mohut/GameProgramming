using UnityEngine;

public class BorderManager : MonoBehaviour
{
    [SerializeField] private GameObject[] borders;
    public static BorderManager Instance;

    private void Start()
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

    public void CheckBorders()
    {
        bool allActivated = true;
        
        foreach (var border in borders)
        {
            if (!border.GetComponent<Border>().activated)
            {
                allActivated = false;
            }
        }

        if (allActivated)
            AimingShoot.Instance.specialShootReady = true;
    }

    public void ResetAllBorders()
    {
        foreach (var border in borders)
        {
            border.GetComponent<SpriteRenderer>().color = Color.red;
            border.GetComponent<Border>().activated = false;
        }
    }
}
