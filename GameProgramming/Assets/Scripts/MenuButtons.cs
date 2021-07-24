
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        int points = Score.Instance.score;
        string name = inputField.text;
        SaveManager.Instance.pointsList.Add(points, name);
        SaveManager.Instance.Save();
        SceneManager.LoadScene(0);
    }
}
