using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    [SerializeField] private TextMeshProUGUI textUI;
    public static Score Instance;
    private GameObject canvas;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        score = 0;
        canvas = GameObject.Find("UI");
    }
    
    

    public void AddPoints(int points, GameObject enemy)
    {
        score += points;
        TextMeshProUGUI text = Instantiate( textUI, Camera.main.WorldToScreenPoint(enemy.transform.position), Quaternion.identity);
        text.text = points.ToString();
        text.transform.parent = canvas.transform;
        StartCoroutine(Animate(text));
    }

    IEnumerator Animate(TextMeshProUGUI text)
    {
        text.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 50f);
        yield return new WaitForSeconds(0.5f);
        Destroy(text.gameObject);
    }
    
}
