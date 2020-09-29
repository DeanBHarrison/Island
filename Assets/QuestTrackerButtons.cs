using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrackerButtons : MonoBehaviour
{
    public int buttonValue;

    public void Press()
    {
            if (!QuestManager.instance.quests[buttonValue].currentlyTracking)
            {
                QuestManager.instance.quests[buttonValue].currentlyTracking = true;
             
            }
            else if (QuestManager.instance.quests[buttonValue].currentlyTracking)
            {
                QuestManager.instance.quests[buttonValue].currentlyTracking = false;
            }
    }
}
