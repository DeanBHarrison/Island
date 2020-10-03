using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Security.Cryptography.X509Certificates;

public class QuestManager : MonoBehaviour
{

    public static QuestManager instance;
    public NPC npc;
    public int current;

    [Header("Quest Log")]
    public List<GameObject> questsInLog = new List<GameObject>();
    public List<Toggle> toggles = new List<Toggle>();
    public GameObject questLogPrefab;
    public GameObject questLogParent;

    

    [Header("Quest tracker")]
    public List<GameObject> questBeingTracked = new List<GameObject>();
    public GameObject questTrackerPrefab;
    public GameObject questTrackerParent;

    public List<Quest> quests = new List<Quest>();


    //
    // delete my crappy method of doing things
    // create a class on the objects I instantiate to update text/images 
    // if a button if toggled off delete 1 object and then re run  the text/image update
    public void toggleClick()
    {
        Debug.Log("toggle");
        Debug.Log("quest count" + questsInLog.Count);
        for (int i = 0; i < questsInLog.Count; i++)
        {
            if (!toggles[i].isOn)
            {

                Debug.Log("destroy??");
                Destroy(questBeingTracked[i]);
                questBeingTracked.Remove(questBeingTracked[i]);

            }
        }
        //UpdateQuestTracker();
    }

    public void UpdateQuestTracker()
    {
        Debug.Log("toggle");
        Debug.Log("update tracker quest count " + quests.Count);
        //instantiate gameobjects for every quest
        for (int i = 0; i < quests.Count; i++)
        {
           

           // if (quests.Count > questBeingTracked.Count )
           // {
               
                GameObject G = Instantiate(questTrackerPrefab, questTrackerParent.transform);
                questBeingTracked.Add(G);
             
            //}
        }

        int x = 0;

        foreach (GameObject quest in questBeingTracked)
        {
            if (toggles[x].isOn)
            {
                quest.GetComponentsInChildren<TextMeshProUGUI>()[0].SetText(quests[x].description);
                quest.GetComponentInChildren<Text>().text = quests[x].goal.currentAmount + "/" + quests[x].goal.requiredAmount;
            }

            x++;
        }
    }

    public void Start()
    {
        //SetUpTrackerbuttons();
    }

    /*public void SetUpTrackerbuttons()
    {
        
        for (int i = 0; i < toggles.Count; i++)
        {
            // gives each of the 54 buttons a number index in it's buttonvalue field from 1 to 54
            toggles[i].buttonValue = i;
        }
    }*/



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


        for (int i = 0; i < quests.Count; i++)
        {
            if (quests.Count > questsInLog.Count)
            {

                GameObject G = Instantiate(questLogPrefab, questLogParent.transform);
                questsInLog.Add(G);
                toggles.Add(G.GetComponentInChildren<Toggle>());
            }
        }

        int x = 0;

        foreach (GameObject quest in questsInLog)
        {
            if (x < quests.Count)
            {
                quest.GetComponentInChildren<TextMeshProUGUI>().SetText(quests[x].description);
                
                
            }
            else
            {
                questsInLog.Remove(quest);
            }
            x++;
        }


    }

   /* public void UpdateQuestTracker()
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
    }*/



    public void CheckIfItemNeededForQuest(Pickups ItemPickedUp)
    {
        //cycle through current quests
        for (int i = 0; i < quests.Count; i++)
        {
            //if item picked up is the same as the item required
            if (ItemPickedUp.pickupName == quests[i].goal.PickupRequired && quests[i].isActive)
            {
                //increase the item tally on quest goals
                quests[i].goal.ItemGathered();
            }


            CheckIfQuestComplete();

            // update tracker


        }
        //UpdateQuestTracker();
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
                //UpdateQuestTracker();
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
