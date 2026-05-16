using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/Quest")]

public class Quest : ScriptableObject
{
    [Header("Quest Data")]
    public int questID = 0;
    public string questName = string.Empty;
    public string questDescription = string.Empty;
    public bool isQuestDone = false;
}
