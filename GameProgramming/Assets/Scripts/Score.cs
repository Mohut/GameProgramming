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

    public void AddPoints(int points, Vector2 position, GameObject enemy)
    {
        score += points;
        TextMeshProUGUI text = Instantiate( new TextMeshProUGUI(), new Vector2(0,0), Quaternion.identity);
        text.text = points.ToString();
        Destroy(enemy);
    }
}
