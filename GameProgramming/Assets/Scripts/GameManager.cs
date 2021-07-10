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
        if (BulletManager.Instance.noBulletsLeft)
        {
            GameOverTime -= Time.deltaTime;
        }
        else
        {
            GameOverTime = 3;
        }
        
        Debug.Log(GameOverTime);

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
