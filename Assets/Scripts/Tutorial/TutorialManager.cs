using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private TextMeshProUGUI tutorialTitle;
    private int playerCollectibleCount;
    private int defeatedEnemyCount;
    
    public void CollectibleGot()
    {
        playerCollectibleCount++;
    }

    public void DefeatedEnemy()
    {
        defeatedEnemyCount++;
    }

    private void Update()
    {
        
    }

    public void CloseTutorialWindow()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1.0f;
        if (playerCollectibleCount > 0 && defeatedEnemyCount > 0)
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("MainGame");
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowTutorialWindow(string _tutorialText, string _tutorialTitle)
    {
        tutorialPanel.SetActive(true);
        tutorialText.text = _tutorialText;
        tutorialTitle.text = _tutorialTitle;
        Time.timeScale = 0f;
    }
}
