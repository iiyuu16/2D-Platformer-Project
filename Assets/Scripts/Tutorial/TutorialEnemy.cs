using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TutorialEnemy : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private string tutorialTitle;
    [SerializeField] private string tutorialDescrip;
    private void OnDisable()
    {
        tutorialManager.ShowTutorialWindow(tutorialDescrip, tutorialTitle);
        tutorialManager.DefeatedEnemy();
    }
}
