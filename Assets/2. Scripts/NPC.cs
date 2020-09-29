using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NPC : MonoBehaviour
{
    public ButtonSelect x;


    [Header("Quests")]
    public bool QuestToGive;
    public Transform QuestIcon;
    public Quest quest;

    [Header("material Glow")]
    public Material standardMat;
    public Material GlowMat;
    public GameObject spotLight;
    public SpriteRenderer theSpriterenderer;

    [Header("chat bubble")]
    public string hoverChat;
    public Transform npcTransform;

    [Header("Dialogue")]
    public bool allowDialogueTrigger;
    public Dialogue dialogue;
    public Dialogue questDialogue;

    [Header("Shop")]
    public string[] itemsForSale = new string[54];

    public string npcName = "NPC NAME HERE";
    NpcDataSaveData npcDataSave;

    public void LoadData()
    {
        npcDataSave = SaveLoadSystem.LoadData(npcName);
        QuestToGive = npcDataSave.QuestToGive;
    }

    public void QuestAvailable()
    {
        if (QuestToGive)
        {
            QuestIcon.gameObject.SetActive(true);
        }
        else
        {
            QuestIcon.gameObject.SetActive(false);
        }
    }

    public void QuestAccepted()
    {
        
        QuestToGive = false;
        SaveLoadSystem.SaveData(npcName, QuestToGive);
        QuestIcon.gameObject.SetActive(false);
    }

    #region
    public void Chatter()
    {
        ChatBubble.Create(npcTransform, new Vector2(0, 1f), hoverChat);
    }

    public void OnMouseEnter()
    {
      
            theSpriterenderer.material = GlowMat;
            spotLight.SetActive(true);
        
    }

    public void OnMouseExit()
    {
        theSpriterenderer.material = standardMat;
        spotLight.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !DialogueManager.instance.ChatActive)
        {
            allowDialogueTrigger = true;
            DialogueManager.instance.buttons = x;

            if (QuestToGive)
            {
                DialogueManager.instance.SetChatOption(questDialogue.sentences.Length);
                GameMenu.instance.SetQuestInfo(quest.title, quest.description, quest.RewardSprite, quest.goal.objectiveSprite);
            }else
            {
                DialogueManager.instance.SetChatOption(dialogue.sentences.Length);
            }
            QuestManager.instance.npc = this;
            DialogueManager.instance.npc = this;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            allowDialogueTrigger = false;
        }
    }

    public void OnMouseUpAsButton()
    {
        if (PlayerController.instance.canMove && allowDialogueTrigger && !EventSystem.current.IsPointerOverGameObject())
        {
           
                TriggerDialogue();
        }

    }
    public void TriggerDialogue()
    {
        if (QuestToGive)
        {
            DialogueManager.instance.StartDialogue(questDialogue);
        }else
        {
            DialogueManager.instance.StartDialogue(dialogue);
        }
    }
    #endregion


    //Triggers the dialogue, feds in the chat dialogue through parameter



}
