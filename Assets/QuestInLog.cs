using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuestInLog : MonoBehaviour
{
#pragma warning disable 0649
    public TextMeshProUGUI questTitle;
    public Toggle toggle;
#pragma warning restore 0649

    // script goes on each quest GameObject
    // creates setup method which can be called in  quest manager 
    // Sets description / objective tracking / objective image

    public void Setup(Quest quest)
    {
        questTitle.SetText(quest.description);
        QuestManager.instance.toggles.Add(toggle);
    }

    public void togglePress()
    {
        QuestManager.instance.UpdateQuestsToTrack();
        Debug.Log("tried to update quests to track");
    }


}
