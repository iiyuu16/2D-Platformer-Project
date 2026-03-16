using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectibleTutorial : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private string tutorialText;
    [SerializeField] private string tutorialTitle;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tutorialManager.ShowTutorialWindow(tutorialText, tutorialTitle);
            tutorialManager.CollectibleGot();
        }
    }
}
