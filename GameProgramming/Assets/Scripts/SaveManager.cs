using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public HighscoreList pointsList;
    [SerializeField] private List<TextMeshProUGUI> pointsText;
    [SerializeField] private List<TextMeshProUGUI> namesText;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }

        pointsList = Load();
        LoadHightscoreList();
    }

    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saves.deano";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, pointsList);
        stream.Close();
    }

    public HighscoreList Load()
    {
        string path = Application.persistentDataPath + "/saves.deano";
        
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            HighscoreList result = formatter.Deserialize(stream) as HighscoreList;
            stream.Close();
            
            return result;
        }
        
        Debug.Log("Save file not found in " + path);
        return new HighscoreList();
    }

    public void LoadHightscoreList()
    {
        
        foreach (var text in pointsText)
        {
            text.text = "-";
        }
        
        if (pointsList.pointsList != null)
        {
            if (pointsList.pointsList.Count <= 3)
            {
                int i = 0;
                
                foreach(var points in pointsList.pointsList)
                {
                    if (i <= pointsList.pointsList.Count-1)
                    {
                        pointsText[i].text = points + " Points";
                        namesText[i].text = pointsList.namesList[i];
                        i++;
                    }
                }
            }
            else
            {
                int i = 0;
                
                foreach(var points in pointsList.pointsList)
                {
                    if (i <= 2)
                    {
                        pointsText[i].text = points + " Points";
                        namesText[i].text = pointsList.namesList[i];
                        i++;
                    }
                }
            }
        }
    }
}
