using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float GameOverTime;
    private TextMeshProUGUI timeText;
    [SerializeField] private RawImage endGameScreen;
    [SerializeField] private TextMeshProUGUI pointsText;

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
        endGameScreen.gameObject.SetActive(true);
        
        pointsText.text = Score.Instance.score.ToString();
    }
}
