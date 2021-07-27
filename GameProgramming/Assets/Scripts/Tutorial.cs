using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    [SerializeField] private GameObject mouse;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    private bool tutorialshowed;
    
    void Start()
    {
        tutorialshowed = false;
        Invoke(nameof(RightClickTutorial), 2);
    }

    private void Update()
    {
        if (Player.Instance.specialShootReady && !tutorialshowed)
        {
            StartCoroutine(SpecialTutorial());
        }
    }

    // removes the tutorial images
    public void RightClickTutorial()
    {
        mouse.SetActive(false);
        leftArrow.SetActive(false);
    }

    // Shows the player to click the right mouse button to use the special attack
    IEnumerator SpecialTutorial()
    {
        tutorialshowed = true;
        mouse.SetActive(true);
        rightArrow.SetActive(true);
        yield return new WaitForSeconds(2);
        mouse.SetActive(false);
        rightArrow.SetActive(false);
    }
}
