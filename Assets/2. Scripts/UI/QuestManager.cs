using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class QuestManager : MonoBehaviour
{

    public static QuestManager instance;
    public NPC npc;
    public int current;

    [Header("Quest Log")]
    public List<TextMeshProUGUI> questTitles = new List<TextMeshProUGUI>();
    public List<QuestTrackerButtons> QuestTrackButtons = new List<QuestTrackerButtons>();


    [Header("Quest tracker")]
    public List<TextMeshProUGUI> QTrackerstitle = new List<TextMeshProUGUI>();
    public List<Image> QTrackersObjective = new List<Image>();
    public List<TextMeshProUGUI> QTrackersProgress = new List<TextMeshProUGUI>();

    public List<Quest> quests = new List<Quest>();


    public void trackQuestYesNO()
    {

    }

    public void Start()
    {
        SetUpTrackerbuttons();
    }

    public void SetUpTrackerbuttons()
    {
        
        for (int i = 0; i < QuestTrackButtons.Count; i++)
        {
            // gives each of the 54 buttons a number index in it's buttonvalue field from 1 to 54
            QuestTrackButtons[i].buttonValue = i;
        }
    }



            public void AcceptQuest()
    {
        quests.Add(npc.quest);
        npc.quest.isActive = true;
        npc.QuestToGive = false;
        GameMenu.instance.questMenu.SetActive(false);
        UpdateQuestLog();
        UpdateQuestTracker();
    }

    public void UpdateQuestLog()
    {
        int x = 0;
       
        
        foreach (TextMeshProUGUI qt in questTitles)
        {
            if (x < quests.Count)
            {
                qt.SetText(quests[x].description);
                QuestTrackButtons[x].gameObject.SetActive(true);
            }
            else
            {
                qt.SetText("");
                QuestTrackButtons[x].gameObject.SetActive(false);
            }
            x++;
        }
    }

    public void UpdateQuestTracker()
    {
        int x = 0;
        int y = 0;
        int z = 0;
        //update titles
        foreach (TextMeshProUGUI qt in QTrackerstitle)
        {
            if (x < quests.Count)
            {
                qt.SetText(quests[x].description);
            }
            else
            {
                qt.SetText("");
            }
            x++;
        }

        //update object sprite
        foreach (Image QI in QTrackersObjective)
        {
            if (y < quests.Count)
            {
                QTrackersObjective[y].gameObject.SetActive(true);
                QI.sprite = quests[y].goal.objectiveSprite;
            }
            else
            {
                QTrackersObjective[y].gameObject.SetActive(false);
            }
            y++;
        }

        //upate progress

        foreach (TextMeshProUGUI QT in QTrackersProgress)
        {
            if (z < quests.Count)
            {
                QTrackersProgress[z].SetText(quests[z].goal.currentAmount.ToString() + "/" + quests[z].goal.requiredAmount.ToString());
            }
            else
            {
                QTrackersProgress[z].SetText("");
            }
            z++;
        }
    }



    public void CheckIfItemNeededForQuest(Pickups ItemPickedUp)
    { 
        //cycle through current quests
        for(int i = 0; i < quests.Count; i++)
        {
            //if item picked up is the same as the item required
            if(ItemPickedUp.pickupName == quests[i].goal.PickupRequired && quests[i].isActive)
            {
                //increase the item tally on quest goals
                quests[i].goal.ItemGathered();
            }

            
            CheckIfQuestComplete(); 

            // update tracker


        }
        UpdateQuestTracker();
    }


    private void CheckIfQuestComplete()
    {
        for (int i = 0; i < quests.Count; i++)
        {
            //IF THE QUEST IS COMPLETE - REMOVE IT FROM LIST AND GIVE REWARDS
            if (quests[i].goal.goalIsReached)
            {
                //quest is complete 
                Debug.Log("Quest complete");

                GameMenu.instance.questRewardMenu.SetActive(true);
                GameMenu.instance.questRewardImage.sprite = quests[i].RewardSprite;
                GameMenu.instance.questTitleText.SetText(quests[i].title);


            }
        }
    }


    public void CompleteQuestButton()
    {
        for (int i = 0; i < quests.Count; i++)
        {
            //IF THE QUEST IS COMPLETE - REMOVE IT FROM LIST AND GIVE REWARDS
            if (quests[i].goal.goalIsReached)
            {
                //quest is complete 
                Debug.Log("Quest complete");

                foreach (var item in quests[i].coinReward)
                {
                    var go = Instantiate(item, GameMenu.instance.transform);

                    go.transform.position = Input.mousePosition;
                }
                //remove quests from list
                // questTitles[i].SetText("");
                quests.Remove(quests[i]);
                UpdateQuestLog();
                UpdateQuestTracker();
                GameMenu.instance.questRewardMenu.SetActive(false);
            }
        }

    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
