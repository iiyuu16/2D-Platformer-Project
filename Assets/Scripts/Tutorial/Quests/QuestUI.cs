using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI questTitle;
    [SerializeField]private TextMeshProUGUI questText;
    public int questID;

    private void OnEnable()
    {
        
    }

    public void SetQuestDetails(int ID, string questName, string questDesc)
    {
        questID = ID;
        questTitle.text = questName;
        questText.text = questDesc;
    }

    public void SetQuestDone()
    {
        questText.text = questText.text + " (Done)";
    }
}
