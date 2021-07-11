using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float GameOverTime;

    private void Start()
    {
        GameOverTime = 3;
    }

    private void Update()
    {
        if (false)
        {
            GameOverTime -= Time.deltaTime;
        }
        else
        {
            GameOverTime = 3;
        }

        if (GameOverTime <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
    }
}
