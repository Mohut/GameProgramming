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

    // Checks if the player can use his special attack
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
            Player.Instance.specialShootReady = true;
    }

    // Resets all borders if the player uses his special attack
    public void ResetAllBorders()
    {
        foreach (Border border in borders)
        {
            border.ResetBorder();
        }
    }
}
