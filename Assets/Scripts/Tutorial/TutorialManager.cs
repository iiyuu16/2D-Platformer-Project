using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private TextMeshProUGUI tutorialTitle;
    [SerializeField] GameObject endObject;
    [SerializeField] GameObject enemyObject;
    [SerializeField] private Quest[] quests;
    [SerializeField] private GameObject questPrefab;
    [SerializeField] private GameObject questList;
    private List<QuestUI> questObjects = new List<QuestUI>();
    private int questCounter;
    private int playerCollectibleCount;
    private int defeatedEnemyCount;
    public bool doorUnlocked = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void OnEnable()
    {
        foreach (Quest quest in quests)
        {
            GameObject questPrefabInstantiated = Instantiate(questPrefab, questList.transform);
            QuestUI questUI = questPrefabInstantiated.GetComponent<QuestUI>();
            questUI.SetQuestDetails(quest.questID, quest.questName, quest.questDescription);
            questObjects.Add(questUI);
        }
    }

    public void CollectibleGot()
    {
        playerCollectibleCount++;
        enemyObject.SetActive(true);
        foreach(QuestUI quest in questObjects)
        {
            if(quest.questID == 2)
            {
                quest.SetQuestDone();
                questCounter++;
            }
        }
    }

    public void FinishedMovementTutorial()
    {
        foreach (QuestUI quest in questObjects)
        {
            if (quest.questID == 0)
            {
                quest.SetQuestDone();
                questCounter++;
            }
        }
    }

    public void DefeatedEnemy()
    {
        defeatedEnemyCount++;
        foreach (QuestUI quest in questObjects)
        {
            if (quest.questID == 1)
            {
                quest.SetQuestDone();
                questCounter++;
            }
        }
    }

    public void OpenedInventory()
    {
        foreach (QuestUI quest in questObjects)
        {
            if (quest.questID == 3)
            {
                quest.SetQuestDone();
                questCounter++;
            }
        }
    }

    private void Update()
    {
        if (questCounter < questObjects.Count)
            return;
        questList.SetActive(false);
        if (!doorUnlocked)
            return;
        SceneManager.LoadScene("MainGame");
    }

    public void CloseTutorialWindow()
    {
        tutorialPanel.SetActive(false);
        Time.timeScale = 1.0f;
        if (playerCollectibleCount > 0 && defeatedEnemyCount > 0)
        {
            Time.timeScale = 1.0f;
            LightManager.Instance.SetLightIntensity(0f);
            endObject.SetActive(true);
        }
    }

    public void ShowTutorialWindow(string _tutorialText, string _tutorialTitle)
    {
        tutorialPanel.SetActive(true);
        tutorialText.text = _tutorialText;
        tutorialTitle.text = _tutorialTitle;
        Time.timeScale = 0f;
    }
}
