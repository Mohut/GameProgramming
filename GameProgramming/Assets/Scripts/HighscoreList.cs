using System;
using System.Collections.Generic;
using System.Linq;


[Serializable]
public class HighscoreList
{
    public List<int> pointsList;
    public List<string> namesList;

    public void AddNewPoints(int points, string name)
    {
        if (pointsList == null)
        {
            pointsList = new List<int>();
            namesList = new List<string>();
        }
        
        if (pointsList.Count < 4)
        {
            pointsList.Add(points);
            namesList.Add(name);
        }
        else if(pointsList.Last() < points)
        {
            pointsList.Remove(pointsList.Last());
            namesList.Remove(namesList.Last());
            pointsList.Add(points);
            namesList.Add(name);
        }

        if (pointsList.Count > 1)
        {
            for (int j = pointsList.Count - 2; j >= 0; j--)
            {
                for (int i = 0; i <= j; i++)
                {
                  if (pointsList[i] < pointsList[i + 1])
                  {
                      var number = pointsList[i];
                      pointsList[i] = pointsList[i+1];
                      pointsList[i + 1] = number;
                                      
                      var a = namesList[i];
                      namesList[i] = namesList[i+1];
                      namesList[i + 1] = a;
                  }  
                }
            }
        }
    }
}
