using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // triggers dialogue to start

    //adding this public Dialogue (which is a reference to another script without monobehavior) adds this script to the unity inspector
    // as if the script was actually added to the object
    public Dialogue dialogue;
    // bool to check if player is close enough to the NPC to trigger dialogue
    private bool allowDialogueTrigger;



    // trigger dialogue when NPC is clicked on
    public void OnMouseUpAsButton()
    {
        if (PlayerController.instance.canMove && allowDialogueTrigger)
        {
            //starts the dialogue on the dialogue manager
            TriggerDialogue();
        }
        Debug.Log("clicked on NPC - on mouse up");

    }


    //Triggers the dialogue, feds in the chat dialogue through parameter
    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialogue);
    }


    // triggers to see if player is close enough to NPC
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            allowDialogueTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            allowDialogueTrigger = false;
        }
    }
  
}
