using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    private TextMeshProUGUI text;
    public static Score Instance;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        score = 0;
        text = GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        text.text = score.ToString();
    }

    public void AddPoints(int points)
    {
        score += points;
    }
}
