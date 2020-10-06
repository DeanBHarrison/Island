using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMenu : MonoBehaviour
{

    //GAME MENU - this script should have all the gameobjects required for the functionality of all the UI menus.

    public static GameMenu instance;
    public GameObject gameMenu;
    public GameObject petImage;
    public GameObject shopMenu;

    [Header("Quest menu")]
    public GameObject questMenu;
    public Image rewardImage;
    public Image objectiveImage;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public GameObject questLog;
    public GameObject questParent;

    [Header("Quest reward")]
    public GameObject questRewardMenu;
    public Image questRewardImage;
    public TextMeshProUGUI questTitleText;

    [Header("Quest tracker")]
    public GameObject qTrackerMenu;
    public TextMeshProUGUI qTrackerTitleString;
    public TextMeshProUGUI qTrackerRewardString;
    public TextMeshProUGUI qTrackercurrentValue;
    public TextMeshProUGUI qTrackermaxValue;


    [Header("bottom right menus")]
    public GameObject petStatsMenu;
    public GameObject itemsMenu;
    public GameObject MapMenu;
    public bool menuIsActive;

    [Header("Key Attribute menus")]
    public GameObject StrengthMenu;
    public GameObject SpeedMenu;
    public GameObject IntellectMenu;
    public GameObject CharmMenu;

    [Header("Key Attribute text")]
    public Text KAStrength;
    public Text KAspeed;
    public Text KACharm;
    public Text KAIntellect;


    public Text goldText;

    [Header("Menu stats gameobjects")]
    public Text maxRedMenu;
    public Text maxBlueMenu;
    public Text strengthMenu;
    public Text speedMenu;
    public Text intellectMenu;
    public Text charmMenu;

    [Header("Items")]
    public itemButton[] itemButtons;
    public string selectedItem;
    public Item activeItem;
    public Text itemName, itemDescription, useButtonText;
    public GameObject itemDescriptionBox;
    public GameObject itemActionBox;

    public void closeActionButton()
    {
        itemActionBox.SetActive(false);
    }

    private void Awake()
    {
        SetUpSingleton();
    }
    // Start is called before the first frame update
    private void Start()
    {
        SetCurrentGold();
        InvokeRepeating("SetKeyAttributeStatText", 0.01f, 0.2f);
        InvokeRepeating("setPetStatsOnMenu", 0.01f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void SetQuestInfo(string title, string description, Sprite rewardSprite, Sprite objectiveSprite)
    {
        titleText.SetText(title);
        descriptionText.SetText(description);
        rewardImage.sprite = rewardSprite;
        objectiveImage.sprite = objectiveSprite;
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
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }


    // updates the menu text to be the pets current stats / and current gold / and sets image
    public void setPetStatsOnMenu()
    {
        petImage.GetComponent<Image>().sprite = PetSpawner.instance.SelectedPetSO.artwork;
        SetCurrentGold();
        maxRedMenu.text = PetStats.instance.maxRed.ToString();
        maxBlueMenu.text = PetStats.instance.maxBlue.ToString();
        strengthMenu.text = PetStats.instance.strength.ToString();
        speedMenu.text = PetStats.instance.speed.ToString();
        intellectMenu.text = PetStats.instance.intellect.ToString();
        charmMenu.text = PetStats.instance.charm.ToString();
    }

    public void SetKeyAttributeStatText()
    {
        //KAStrength.text = PetStats.instance.strength.ToString();
       // KAspeed.text = PetStats.instance.speed.ToString();
       // KAIntellect.text = PetStats.instance.intellect.ToString();
       // KACharm.text = PetStats.instance.charm.ToString();
    }

    public void SetCurrentGold()
    {
        goldText.text = InventoryManager.instance.currentGold.ToString() + " g";

    }


    public void closeAllMenus()
    {
        shopMenu.SetActive(false);
        gameMenu.SetActive(false);
        itemsMenu.SetActive(false);
        petStatsMenu.SetActive(false);
        MapMenu.SetActive(false);
        questLog.SetActive(false);
        menuIsActive = false;
    }

    public void MapButton()
    {
        if (!MapMenu.activeInHierarchy)
        {
            closeAllMenus();
            gameMenu.SetActive(true);
            MapMenu.SetActive(true);
            menuIsActive = true;
        }
        else
        if (MapMenu.activeInHierarchy)
        {
            closeAllMenus();
        }
    }

    public void QuestLogButton()
    {
        if (!questLog.activeInHierarchy)
        {
            QuestManager.instance.UpdateQuestLog();
            QuestManager.instance.UpdateQuestsToTrack();
            
            closeAllMenus();
            questLog.SetActive(true);
            menuIsActive = true;
        }
        else
        if (questLog.activeInHierarchy)
        {
            closeAllMenus();
        }
    }
    public void itemButton()
    {
        if (!itemsMenu.activeInHierarchy)
        {
            //first close all menus incase another menu is open
            closeAllMenus();

            //sort and set up items in ventory before opening
            InventoryManager.instance.SortItems();
            ShowItems();

            // then open menus
            gameMenu.SetActive(true);
            itemsMenu.SetActive(true);

            // keep track of current menu state
            menuIsActive = true;
        }
        else
        if (itemsMenu.activeInHierarchy)
        {
            closeAllMenus();
        }
    }

    public void StatsButton()
    {
        if (!petStatsMenu.activeInHierarchy)
        {
            closeAllMenus();
            setPetStatsOnMenu();
            gameMenu.SetActive(true);
            petStatsMenu.SetActive(true);
            menuIsActive = true;
        }
        else
        if (petStatsMenu.activeInHierarchy)
        {
            closeAllMenus();
        }


    }

    public void SleepButton()
    {
        if (PetStats.instance.currentSleepiness >= 80)
        {
            Clock.instance.PassTime(PetStats.instance.sleepLength);
            PetStats.instance.currentSleepiness = 0;
            Clock.instance.CheckIfItShouldBeDark();
            Clock.instance.shouldTimePass = true;
        }
    }

    public void PlayButton()
    {
        Clock.instance.SpeedupTime(4, 1);
        MusicPlayer.instance.PlaySFX(6);
        PetStats.instance.currentHapiness -= 30;
    }

  


    // sets the images and info of current held items and gets rid of the images if theyre empty
    public void ShowItems()
    {
        for (int i = 0; i < itemButtons.Length; i++)
        {
            // gives each of the 54 buttons a number index in it's buttonvalue field from 1 to 54
            itemButtons[i].buttonValue = i;

            // for every item that has a string name in the items held array, set the image object to be active, set the sprite to what it should be,
            if (InventoryManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                // here we have to get the script information for the specific item from the reference item database, so we have to call a function that searches the database given the string of the item we have.
                itemButtons[i].buttonImage.sprite = InventoryManager.instance.GetItemDetails(InventoryManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = InventoryManager.instance.numberOfItems[i].ToString();
            }
            else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    // method to set variable activeitem which is used to manipulate items from game, set through other scripts when called using the parameter.
    // this sets the item name and description when you click on the item
    public void SelectItem(Item newItem)
    {
        activeItem = newItem;

        if (activeItem.isFood)
        {
            useButtonText.text = "Eat";
        }
        else if (activeItem.isItem)
        {
            useButtonText.text = "Use";
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
    }

    // drops current item
    public void DiscardItem()
    {
        if (activeItem != null)
        {
            InventoryManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void UseItembutton()
    {
        activeItem.UseItem();
    }
}
