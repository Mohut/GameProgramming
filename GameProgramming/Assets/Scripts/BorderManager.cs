using UnityEngine;

public class BorderManager : MonoBehaviour
{
    [SerializeField] private Border[] borders;
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
            if (!border.activated)
            {
                allActivated = false;
            }
        }

        if (allActivated)
            AimingShoot.Instance.specialShootReady = true;
    }

    public void ResetAllBorders()
    {
        foreach (Border border in borders)
        {
            border.ResetBorder();
        }
    }
}
