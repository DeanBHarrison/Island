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

    //game object if a yes/no option needs to be triggered
    public GameObject YesNoButton;

    // bool to trigger yes/no option or not.
    public bool YesNoButtonRequired;


    private void Start()
    {
        //checks to see if the button should be spawned every 0.5seconds, not perfect practice as its not triggered by an action and is constant running
        // but shouldnt be too bad as invoke repeating isnt very harsh ont he system.
        InvokeRepeating("SpawnButton", 0.01f, 0.5f);
    }


    private void Update()
    {

        // things that rely on user input generally need to be in update to register on the frame that the user inputs them.
        if(allowDialogueTrigger && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove)
        {
            //starts the dialogue on the dialogue manager
            TriggerDialogue();           
        }

       
    }


    //spawns the attached yes/no button when there is only 1 sentence left for the NPC to say.

    // i had to adjust the code to trigger on count = 1 not count = 0 because 0 is the default and it was messing things up
    // this means I need to add a blank line of chat to every NPCs dialogue.
    private void SpawnButton()
    {
        if (DialogueManager.instance.sentences.Count == 1 && YesNoButtonRequired)
        {
            YesNoButton.SetActive(true);
        }else

        //makes the button inactive if its not the last sentence
        if (DialogueManager.instance.sentences.Count > 1 && YesNoButtonRequired)
        {
            YesNoButton.SetActive(false);
        }
    }


    // if the player picks NO out of the options the dialogue moves along one line and the button is set to inactive.
    public void NoButton()
    {
        DialogueManager.instance.DisplayNextSentence();
        YesNoButton.SetActive(false);
    }

    //Triggers the dialogue, feeds in the chat dialogue through parameter
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
