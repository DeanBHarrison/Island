using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NPC  : MonoBehaviour
{
    public ButtonSelect x;


    [Header("Quests")]
    public bool QuestToGive;
    public Transform QuestIcon;
    public Quest quest;
    public Image rewardImage;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public GameObject questWindow;
    public NPC activeNPC;


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

    [Header("Shop")]
    public string[] itemsForSale = new string[54];


  
    public void Chatter()
    {
        ChatBubble.Create(npcTransform, new Vector2(0, 1f), hoverChat);
    }

    public void OnMouseEnter()
    {
        DialogueManager.instance.buttons = x;
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
        if (other.tag == "Player")
        {
            allowDialogueTrigger = true;
            DialogueManager.instance.SetChatOption(dialogue.sentences.Length);
            if (QuestToGive)
            {
                SetQuest();
            }
            QuestManager.instance.npc = this;
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
        if (PlayerController.instance.canMove && allowDialogueTrigger)
        {
            TriggerDialogue();
        }

    }

    public void SetQuest()
    {
        titleText.SetText(quest.title);
        descriptionText.SetText(quest.description);
        rewardImage.sprite = quest.RewardSprite;
    }


    //Triggers the dialogue, feds in the chat dialogue through parameter
    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }
}
