using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestTracker : MonoBehaviour
{

#pragma warning disable 0649
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI objectiveCount;
    public Image objectiveImage;
#pragma warning restore 0649

    // script goes on each quest GameObject
    // creates setup method which can be called in  quest manager 
    // Sets description / objective tracking / objective image

    public void Setup(Quest quest)
    {
        objectiveImage.sprite = quest.goal.objectiveSprite;
        questTitle.SetText(quest.description);
        objectiveCount.SetText(quest.goal.currentAmount + " / " + quest.goal.requiredAmount);
    }
}


