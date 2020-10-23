using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class TechController : MonoBehaviour
{

    [Header("Description box objects")]
    public GameObject descriptionBox;
    public Text DescriptionBoxText;
    public Text Energycost;
    public Image TechImage;

    public Text currentHandSizeText;
    public Text maxHandSizeText;

    public static TechController instance;
    // Keeps track of the current cards in player deck, uses string
    public List<string> Deck;
    //a reference of all possible techs to look up data against
    public Techs[] TechsReference;
    //the Gameobject with the current hand in the UI
    public GameObject[] HandGO;
    // the Tech script for the current hand
    public Techs[] DiscardPile;
    // the Tech script for the current hand
    public Techs[] HandTechs;
    //deck size - solution to Deck being an array with gaps in it, we sort it and then only use up to deck size for for loops
    public int deckSize;

    public Techs TechLastUsed;
    
    public int maxHandSize = 2;
    public int currentHandSize = 0;


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
                Destroy(gameObject);
                Debug.Log("something messed up with tech controller");
            }
        }
    }
    private void Awake()
    {
        SetUpSingleton();
    }
    private void Start()
    {

        

        deckSize = Deck.Count;
        //this needs to be called first or else everthing else wont work. sorts and figures out current deck size.
        SortTechs();
        DrawCard(3);
        updateHandCounter();
    }

    public void updateHandCounter()
    {
        currentHandSizeText.text = currentHandSize.ToString() + " / " ;
            maxHandSizeText.text = maxHandSize.ToString();
    }

    public void UseTech0()
    {
        if (!battleSystem.instance.playerUnit.SpendEnergy(HandTechs[0].EnergyCost))
        {
            Debug.Log("tech 0 used");
            TechLastUsed = HandTechs[0];
            DiscardTech(HandTechs[0]);
            HandGO[0].GetComponent<Image>().sprite = null;
            battleSystem.instance.TechPlayed(HandTechs[0]);
            HandTechs[0] = null;
            currentHandSize--;
            SortHandTechs();
            updateHandCounter();
            descriptionBox.SetActive(false);
        }
        else
        {
            Debug.Log("out of energy");
        }
        
    }

    public void UseTech1()
    {
        if (!battleSystem.instance.playerUnit.SpendEnergy(HandTechs[1].EnergyCost))
        {
            Debug.Log("tech 1 used");
            TechLastUsed = HandTechs[1];
            DiscardTech(HandTechs[1]);
            HandGO[1].GetComponent<Image>().sprite = null;
            battleSystem.instance.TechPlayed(HandTechs[1]);
            HandTechs[1] = null;
            currentHandSize--;
            SortHandTechs();
            updateHandCounter();
            descriptionBox.SetActive(false);
        }
        else
        {
            Debug.Log("out of energy");
        }

    }

    public void UseTech2()
    {
        if (!battleSystem.instance.playerUnit.SpendEnergy(HandTechs[2].EnergyCost))
        {
            Debug.Log("tech 2 used");
            TechLastUsed = HandTechs[2];
            DiscardTech(HandTechs[2]);
            HandGO[2].GetComponent<Image>().sprite = null;
            battleSystem.instance.TechPlayed(HandTechs[2]);
            HandTechs[2] = null;
            currentHandSize--;
            SortHandTechs();
            updateHandCounter();
            descriptionBox.SetActive(false);
        }
        else
        {
            Debug.Log("out of energy");
        }

    }

    public void UseTech3()
    {
        if (!battleSystem.instance.playerUnit.SpendEnergy(HandTechs[3].EnergyCost))
        {
            Debug.Log("tech 3 used");
            TechLastUsed = HandTechs[3];
            DiscardTech(HandTechs[3]);
            HandGO[3].GetComponent<Image>().sprite = null;
            battleSystem.instance.TechPlayed(HandTechs[3]);
            HandTechs[3] = null;
            currentHandSize--;
            SortHandTechs();
            updateHandCounter();
            descriptionBox.SetActive(false);
        }
        else
        {
            Debug.Log("out of energy");
        }

    }

    public void UseTech4()
    {
        if (!battleSystem.instance.playerUnit.SpendEnergy(HandTechs[0].EnergyCost))
        {
            Debug.Log("tech 4 used");
            TechLastUsed = HandTechs[4];
            DiscardTech(HandTechs[4]);
            HandGO[4].GetComponent<Image>().sprite = null;
            battleSystem.instance.TechPlayed(HandTechs[4]);
            HandTechs[4] = null;
            currentHandSize--;
            SortHandTechs();
            updateHandCounter();
            descriptionBox.SetActive(false);
        }
        else
        {
            Debug.Log("out of energy");
        }

    }

    //finds the next available slot in the discard pile and puts the tech that was passed through the paramterer into that slot

    public void DiscardTech(Techs discardedTech)
    {
        for(int i = 0; i < DiscardPile.Length; i++)
        {
            if(DiscardPile[i] == null)
            {
                DiscardPile[i] = discardedTech;
                return;
            }
        }
    }

    public void SortHandTechs()
    {
        // Variable to keep track of what's the next empty slot
        int emptySlot = 0;

        // Iterate through each handtech slot that we have
        for (int i = 0; i < maxHandSize; i++)
        {
            // If we see an available item in the inventory, move it to an empty slot
            if (HandTechs[i] != null)
            {
                HandTechs[emptySlot] = HandTechs[i];
                HandGO[emptySlot].GetComponent<Image>().sprite = HandGO[i].GetComponent<Image>().sprite;
                emptySlot++;
            }
        }

        // Populate inventory with "empty" items
        // We start at the next empty slot, and just iterate through the remaining slots left
        for (int i = emptySlot; i < maxHandSize; i++)
        {
            // Reset the values
            HandTechs[i] = null;
            HandGO[i].SetActive(false);
            HandGO[i].GetComponent<Image>().sprite = null;

        }
    }



    public void DrawCard(int CardsToDraw)
    {
        for (int i = 0; i < CardsToDraw; i++)
        {
            if (currentHandSize < maxHandSize)
            {
                // set a number index for cards to draw so that it can be randomed.
                int cardToDraw = Random.Range(0, deckSize);

                // Sets the first card in our hand to be whatever we randomed the card to draw to be above
                // we first have to call the function get tech details which takes in a string and returns the Tech class for that card.


                HandGO[currentHandSize].SetActive(true);
                HandTechs[currentHandSize] = GetTechDetails(Deck[cardToDraw]);
                //sets the game object to have the sprite of the tech just randomed.
                HandGO[currentHandSize].GetComponent<Image>().sprite = HandTechs[currentHandSize].techSprite;

                //remove the drawn card from the deck
                Deck.Remove(Deck[cardToDraw]);

                //reduce deck size - this causes the RNG with card draw to lower above
                deckSize--;

                //add 1 to the current hand size
                currentHandSize++;
                updateHandCounter();
            }

        }
    }

    public void SortTechs()
    {
        var DeckEmptySlotCount = 0;
        // Variable to keep track of what's the next empty slot
        int emptySlot = 0;

        // Iterate through each inventory slot that we have
        for (int i = 0; i < Deck.Count; i++)
        {
            // If we see an available item in the inventory, move it to an empty slot
            if (Deck[i] != "")
            {
                
                Deck[emptySlot] = Deck[i];
                emptySlot++;
            }
        }

        // Populate inventory with "empty" items
        // We start at the next empty slot, and just iterate through the remaining slots left
        for (int i = emptySlot; i < Deck.Count; i++)
        {
            DeckEmptySlotCount++;
            // Reset the values
            Deck[i] = "";
            
            //count the number of empty spaces
            // take it off the deck.count to find number of cards in deck
            deckSize = Deck.Count - DeckEmptySlotCount;

        }

       // for (int i = 0; i < Deck.Count-1; i++)
       // { 
       //     if (Deck[i] == "") Deck.Remove(Deck[i]);
       //     Debug.Log(i);
       // }
    }

    private void Update()
    {
        // Debug.Log(Deck.Count);
        if(Input.GetKeyUp(KeyCode.R))
        {
            DrawCard(1);
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            SortHandTechs();
        }   
    }


    // returns the infomation about a tech from the tech reference array based on the name of the tech you give it.
    public Techs GetTechDetails(string TechToGet)
    {
        for (int i = 0; i < TechsReference.Length; i++)
        {
            if (TechsReference[i].name == TechToGet)
            {
                return TechsReference[i];
            }
        }
        return null;
    }


}
