﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("yes/no Buttons")]
    public GameObject yesNoInn;
    public GameObject yesNoShop;

    [Header("yes/no triggers")]
    public int buttonSpawnIndex;
    public int sentenceSpawnIndex;

    [Header("dialogue objects")]
    //UI elements to update when cycling through dialogue
    public Text nameText;
    public Text dialogueText;

    // bool that is true when chat is currently open
    public bool ChatActive = false;

    //reference to animator to be able to set a bool to trigger the event of it moving 
    public Animator animator;

    // static instance to set up singleton pattern
    public static DialogueManager instance;

    //creates a Queue, this is similar to a list/array, things can be added to the queue the queue can be cleared.
    // its a good option to use for dialogue
    public Queue<string> sentences;

    private void Awake()
    {
        SetUpSingleton();
    }
    void Start()
    {
        //instantiating a new queue for use below
        sentences = new Queue<string>();
    }

    private void Update()
    {

        if(Input.GetKeyDown(KeyCode.H))
        {
            GameMenu.instance.shopMenu.SetActive(true);
            Debug.Log("tried to set shopmenu active");
        }
    }
    // this method is passing a class through as it's paramter, this class holds all the information of what is to be said in the dialogue

    public int currentSentenceCounter;
    public void StartDialogue(Dialogue dialogue)
    {
        //added in to keep track of what sentence we are on so i can make a method to spawn a button on a certain one
        currentSentenceCounter = 0;
        // bool sent to true to enable animator object to move
        animator.SetBool("isOpen", true);

        // bool set to true to disable movement on playercontroller
        ChatActive = true;

        // sets the name of the person talking as the dialogue starts
        nameText.text = dialogue.name;


        // before starting a new dialogue this is clearing the queue from the last time dialogue was used, incase theres still things in it.
        sentences.Clear();

        //for each line of txt (sentence) in the string[] sentences that is on the dialogue script passed through in the original parameter
        foreach(string sentence in dialogue.sentences)
        {
            //this gets all the setences from the attached script and puts them in the queue "sentences"
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {

        //if the remaing count is 0 it means to convesation is over, it then calls EndDialogue and returns from the rest of the method.
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;            
        }


        //removes the current sentence from the queue and sets it to the variable sentence
        string sentence = sentences.Dequeue();
        //add 1 to the sentence counter for spawning options
        currentSentenceCounter++;

        //this is called before starting the coroutine incase the player clicks next before the chat has ended
        StopAllCoroutines();

        // start the corotuine below that loops though the chars one by one
        StartCoroutine(TypeSentence(sentence));
        CheckForChatOptions();

    }

    public void CheckForChatOptions()
    {
        if (currentSentenceCounter == sentenceSpawnIndex)
        {
            switch (buttonSpawnIndex)
            {
                case 0:
                    yesNoInn.SetActive(true);
                    break;

                case 1:
                    yesNoShop.SetActive(true);
                    break;

                default:
                    Debug.Log("something messed up spawning dialogue buttons");
                    break;
            }
        }
    }

    public void SetChatOption(int ButtonSpawnIndex, int SentenceSpawnIndex)
    {
        buttonSpawnIndex = ButtonSpawnIndex;
        sentenceSpawnIndex = SentenceSpawnIndex;
    }

    IEnumerator TypeSentence (string sentence)
    {
        //to begin with the text needs to be empty
        dialogueText.text = "";

        // looping through each char in sentence, to chararray converts a string into an array of chars
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

    }

    void EndDialogue()
    {
        // bool for animator
        animator.SetBool("isOpen", false);

        //bool for playercontroller . can move
        ChatActive = false;
    }

    private void SetUpSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Debug.Log("something screwed up with dialogue manager singleton");
                Destroy(gameObject);
            }
        }
    }

    public void buttonPressNo()
    {
        //disable the button that was activated
        switch (buttonSpawnIndex)
        {
            case 0:
                yesNoInn.SetActive(false);
                break;

            case 1:
                yesNoShop.SetActive(false);
                break;

            default:
                Debug.Log("something messed up despawning dialogue buttons");
                break;
        }
        // carry on the conversation so the chat box closes
        DisplayNextSentence();
    }

    public void buttonPressYesInn()
    {
        //disable the button that was activated
        switch (buttonSpawnIndex)
        {
            case 0:
                yesNoInn.SetActive(false);
                break;

            case 1:
                yesNoShop.SetActive(false);
                break;

            default:
                Debug.Log("something messed up despawning dialogue buttons");
                break;
        }

        Clock.instance.PassTime(PetStats.instance.sleepLength);
            PetStats.instance.currentSleepiness = 0;
            DisplayNextSentence();
    }

    public void buttonPressYesVegVendorReg()
    {
        //GameMenu.instance.shopMenu.SetActive(true);
        //disable the button that was activated
        switch (buttonSpawnIndex)
        {
            case 0:
                yesNoInn.SetActive(false);
                break;

            case 1:
                yesNoShop.SetActive(false);
                break;

            default:
                Debug.Log("something messed up despawning dialogue buttons");
                break;
        }

        GameMenu.instance.shopMenu.SetActive(true);
        GameMenu.instance.menuIsActive = true;
        VegVendor.instance.VegVendorSetupShop();
        DisplayNextSentence();
    }
}
