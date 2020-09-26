using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public static QuestManager instance;
    public NPC npc;
    public int current;

    public List<Quest> quests = new List<Quest>();


    public void AcceptQuest()
    {
        quests.Add(npc.quest);
        GameMenu.instance.questMenu.SetActive(false);

    }

    
   /* public void CheckIfItemNeededForQuest()
    { 

        for(int i = 0; i < quests.Count; i++)
        {
            switch()
        }
        
    }*/


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
