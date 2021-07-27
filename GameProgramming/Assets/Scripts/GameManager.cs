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
    [SerializeField] private TextMeshProUGUI newHighscoreText;
    [SerializeField] private TMP_InputField inputField;

    private void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;
        GameOverTime = 5;
        timeText = GameObject.Find("GameOverTime").GetComponent<TextMeshProUGUI>();
        timeText.text = GameOverTime.ToString();
    }

    // Checks if the player has disc to shoot. If not the player looses after 5 seconds without a disc
    private void Update()
    {
        if (DiscManager.Instance.discs.Count == DiscManager.Instance.maxDiscs)
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

    // When the player looses the score gets saved with the name of the player if he achieved a place in the highscore list
    public void GameOver()
    {
        Time.timeScale = 0;
        endGameScreen.gameObject.SetActive(true);
        if (SaveManager.Instance.pointsList.pointsList != null)
        {
            if (SaveManager.Instance.pointsList.pointsList.Count < 3)
            {
                newHighscoreText.enabled = true;
                inputField.gameObject.SetActive(true);
                pointsText.text = Score.Instance.score + " Points";
                return;
            }
            if (Score.Instance.score > SaveManager.Instance.pointsList.pointsList[2])
            {
                newHighscoreText.enabled = true;
                inputField.gameObject.SetActive(true);
                pointsText.text = Score.Instance.score + " Points";
            }
        }
        else
        {
            newHighscoreText.enabled = true;
            inputField.gameObject.SetActive(true);
        }
        

        pointsText.text = Score.Instance.score + " Points";
    }
}
