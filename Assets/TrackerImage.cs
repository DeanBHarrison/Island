
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TrackerImage : MonoBehaviour
{
    public Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        int x = 0;

        foreach(GameObject quest in  QuestManager.instance.questBeingTracked)
        {
            image.sprite = QuestManager.instance.quests[x].goal.objectiveSprite;
        }

       // if()
        
    }


}
