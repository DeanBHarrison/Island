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
    public GameObject questTrackerPrefab;
    public GameObject questTrackerParent;

    public List<Quest> quests = new List<Quest>();
    public List<Quest> questsToFillTracker = new List<Quest>();
    public List<GameObject> questsInTracker = new List<GameObject>();

    public void UpdateQuestsToTrack()
    {
        questsToFillTracker.Clear();
        int x = 0;
        foreach(Toggle toggle in toggles)
        {
            if(toggle.isOn)
            {
                questsToFillTracker.Add(quests[x]);
            }
        }
        UpdateQuestTracker();
    }


    public void UpdateQuestLog()
    {
        ClearQuestLog();   

        // adds a gameobject to the quest log and adds it to the questsInLog list.
        foreach (Quest quest in quests)
        {
            if (questsInLog.Count < quests.Count)
            {
                GameObject G = Instantiate(questLogPrefab, questLogParent.transform);
                questsInLog.Add(G);

                //calls setup() on QuestInLog game object sets title/adds toggle to list.
                QuestInLog questInLog = G.GetComponent<QuestInLog>();
                questInLog.Setup(quest);
            }           
        }
    }

   
    public void UpdateQuestTracker()
    {
        ClearQuestTracker();

        int x = 0;

        foreach (Quest quest in quests)
        {

            if (questsToFillTracker.Count > questsInTracker.Count)
            {

                Debug.Log("trying to instantiate quest tracker stuff");
                GameObject G = Instantiate(questTrackerPrefab, questTrackerParent.transform);
                questsInTracker.Add(G);

                //calls setup() on questInTracker game object sets title/objective tally/image
                QuestTracker questInTracker = G.GetComponent<QuestTracker>();
                questInTracker.Setup(questsToFillTracker[x]);
                x++;
            }
        }
    }

    public void ClearQuestLog()
    {       
        for (int i = 0; i < questsInLog.Count; i++)
        {
            Destroy(questsInLog[i]);
            Destroy(toggles[i]);           
        }
        questsInLog.Clear();
        toggles.Clear();
    }

    public void ClearQuestTracker()
    {
        for (int i = 0; i < questsInTracker.Count; i++)
        {
            Destroy(questsInTracker[i]);
            
        }
        questsInTracker.Clear();
    }


    public void AcceptQuest()
    {
        //adds the quest from NPC to quest manager quest list
        quests.Add(npc.quest);
        //sets correct info on NPC
        npc.quest.isActive = true;
        npc.QuestToGive = false;
        //enables the accept quest menu
        GameMenu.instance.questMenu.SetActive(false);
        UpdateQuestLog();
        UpdateQuestsToTrack();
    }

    #region quest progress update/completion
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

                quests.Remove(quests[i]);
                GameMenu.instance.questRewardMenu.SetActive(false);
            }
        }

    }

    #endregion

    #region deleted code
    /* public void toggleClick()
     {
         questBeingTracked.Clear();
         questsToFillTracker.Clear();

         int x = 0;

         foreach (Toggle toggle in toggles)
         {
             Debug.Log("Toggle 1");
             if (toggle.isOn)
             {
                 Debug.Log("Toggle 2");
                 questsToFillTracker.Add(quests[x]);
             }
             x++;
         }


         for (int i = 0; i < quests.Count; i++)
         {
             Debug.Log("Toggle 3");
             if (toggles[i].isOn)
             {

                 Debug.Log("Toggle 4");
                 GameObject G = Instantiate(questTrackerPrefab, questTrackerParent.transform);
                 questBeingTracked.Add(G);
             }
         }
     }*/


    // RE DESIGNED THE CODE ABOVE
    /* public void UpdateQuestTracker()
     {
         Debug.Log("toggle");
         Debug.Log("update tracker quest count " + quests.Count);
         //instantiate gameobjects for every quest
         for (int i = 0; i < quests.Count; i++)
         {


            // if (quests.Count > questBeingTracked.Count )
            // {



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
     }*/



    /*public void SetUpTrackerbuttons()
    {
        
        for (int i = 0; i < toggles.Count; i++)
        {
            // gives each of the 54 buttons a number index in it's buttonvalue field from 1 to 54
            toggles[i].buttonValue = i;
        }
    }*/

    /*public void UpdateQuestLog()
   {
       for (int i = 0; i < quests.Count; i++)
       {
           Debug.Log("testtest");
           if (quests.Count > questsInLog.Count)
           {
               Debug.Log("toggle should be added");
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
   }*/

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

    #endregion

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

    public void Start()
    {
        //SetUpTrackerbuttons();
    }
}
