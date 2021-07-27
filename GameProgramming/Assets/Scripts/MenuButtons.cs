using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        SaveData();
        SceneManager.LoadScene(0);
    }

    public void TryAgain()
    {
        SaveData();
        Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
    }

    // Saves the points in the highscore list if the player clicks on "main menu" or "try again"
    public void SaveData()
    {
        int points = Score.Instance.score;
        string name = inputField.text;
        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("empty");
            SaveManager.Instance.pointsList.AddNewPoints(points, "???"); 
        }
        else
        {
            SaveManager.Instance.pointsList.AddNewPoints(points, name); 
        }
        SaveManager.Instance.Save();
    }
    
}