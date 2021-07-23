using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public SortedDictionary<int, string> pointsList;
    [SerializeField] private List<TextMeshProUGUI> pointsText;

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
        if(pointsList.Count > 0)
            pointsList.Remove(pointsList.Keys.First());
    }

    public SortedDictionary<int, string> Load()
    {
        string path = Application.persistentDataPath + "/saves.deano";
        
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SortedDictionary<int, string> result = formatter.Deserialize(stream) as SortedDictionary<int, string>;
            stream.Close();
            
            Debug.Log(result.Count);
            return result;
        }
        
        Debug.Log("Save file not found in " + path);
        return new SortedDictionary<int, string>();
    }

    public void LoadHightscoreList()
    {
        
        foreach (var text in pointsText)
        {
            text.text = "-";
        }
        
        if (pointsList != null)
        {
            int i = pointsList.Count-1;

            string a = "";
            
            foreach(KeyValuePair<int, string> pair in pointsList)
            {
                if (i >= 0){
                 pointsText[i].text = pair.Key + " " + pair.Value;
                 i--;
                }
            }
        }
    }
}
