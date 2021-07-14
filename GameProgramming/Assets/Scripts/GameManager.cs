using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float GameOverTime;
    private TextMeshProUGUI timeText;

    private void Start()
    {
        GameOverTime = 5;
        timeText = GameObject.Find("GameOverTime").GetComponent<TextMeshProUGUI>();
        timeText.text = GameOverTime.ToString();
    }

    private void Update()
    {
        if (BulletManager.Instance.bullets.Count == BulletManager.Instance.maxBullets)
        {
            GameOverTime -= Time.deltaTime;
        }
        else
        {
            GameOverTime = 5;
        }

        if (GameOverTime <= 0)
        {
            GameOver();
        }

        timeText.text = GameOverTime.ToString().Substring(0, 1);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }
}
