using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public static GameMenu instance;
    public GameObject firstMenu;
    public GameObject gameMenu;
    public GameObject petImage;
    public GameObject shopMenu;

    public GameObject petStatsMenu;
    public GameObject itemsMenu;
    public bool menuIsActive;

    [Header("Small Menu Buttons")]
    public GameObject itemsButton;
    public GameObject statsButton;
    public GameObject Sleepbutton;
    public Text goldText;

    [Header("Menu stats gameobjects")]
    public Text maxRedMenu;
    public Text maxBlueMenu;
    public Text strengthMenu;
    public Text gritMenu;
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



    private void Awake()
    {
        SetUpSingleton();
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        openMenu();
    }

    public void feedPet()
    {
        PetStats.instance.GainHunger(-80);
    }



    public void openMenu()
    {
        if (Input.GetButtonDown("Fire2"))
        {           
          if(!firstMenu.activeInHierarchy && PlayerController.instance.canMove)
            {
                menuIsActive = true;
                firstMenu.SetActive(true);
            
            }
            else
           if (firstMenu.activeInHierarchy)
            {
                firstMenu.SetActive(false);
                menuIsActive = false;
            }
            else
           if(gameMenu.activeInHierarchy)
            {
                gameMenu.SetActive(false);
                petStatsMenu.SetActive(false);
                itemsMenu.SetActive(false);
                GameMenu.instance.itemDescriptionBox.SetActive(false);
                GameMenu.instance.itemActionBox.SetActive(false);

                firstMenu.SetActive(true);
            }else
            if (shopMenu.activeInHierarchy)
            {
                shopMenu.SetActive(false);
            }

        }
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
     goldText.text = InventoryManager.instance.currentGold.ToString() + "g";
     maxRedMenu.text = PetStats.instance.maxRed.ToString();
     maxBlueMenu.text = PetStats.instance.maxBlue.ToString();
     strengthMenu.text = PetStats.instance.strength.ToString();
     gritMenu.text = PetStats.instance.grit.ToString();
     speedMenu.text = PetStats.instance.speed.ToString();
     intellectMenu.text = PetStats.instance.intellect.ToString();
     charmMenu.text = PetStats.instance.charm.ToString();
     }

    public void itemButton()
    {
        InventoryManager.instance.SortItems();
        ShowItems();
        firstMenu.SetActive(false);
        gameMenu.SetActive(true);
        itemsMenu.SetActive(true);
        menuIsActive = true;     
    }

    public void StatsButton()
    {
        setPetStatsOnMenu();
        firstMenu.SetActive(false);
        gameMenu.SetActive(true);
        petStatsMenu.SetActive(true);
        menuIsActive = true;

    }

    public void SleepButton()
    {
        if (PetStats.instance.currentSleepiness >= 80)
        {
            Clock.instance.PassTime(PetStats.instance.sleepLength);
            PetStats.instance.currentSleepiness = 0;
            PetStats.instance.currentFatigue = 0;
            FatigueSlider.instance.updateFatigueSlider();
            Clock.instance.CheckIfItShouldBeDark();
            Clock.instance.shouldTimePass = true;
        }
    }
    // sets the images of current held items
    public void ShowItems()
    {
        for(int i = 0; i < itemButtons.Length; i++)
        {
            // gives each of the 54 buttons a number index in it's buttonvalue field from 1 to 54
            itemButtons[i].buttonValue = i;

            // for every item that has a string name in the items held array, set the image object to be active, set the sprite to what it should be,
            if(InventoryManager.instance.itemsHeld[i] != "")
            {
                itemButtons[i].buttonImage.gameObject.SetActive(true);
                // here we have to get the script information for the specific item from the reference item database, so we have to call a function that searches the database given the string of the item we have.
                itemButtons[i].buttonImage.sprite = InventoryManager.instance.GetItemDetails(InventoryManager.instance.itemsHeld[i]).itemSprite;
                itemButtons[i].amountText.text = InventoryManager.instance.numberOfItems[i].ToString();
            }else
            {
                itemButtons[i].buttonImage.gameObject.SetActive(false);
                itemButtons[i].amountText.text = "";
            }
        }
    }

    // method to set variable activeitem which is used to manipulate items from game, set through other scripts when called using the parameter.
    public void SelectItem(Item newItem)
    {
        activeItem = newItem;

        if(activeItem.isFood)
        {
            useButtonText.text = "Eat";
        }
        else if(activeItem.isItem)
        {
            useButtonText.text = "Use";
        }

        itemName.text = activeItem.itemName;
        itemDescription.text = activeItem.description;
    }

    // drops current item
    public void DiscardItem()
    {
        if(activeItem != null)
        {
            InventoryManager.instance.RemoveItem(activeItem.itemName);
        }
    }

    public void UseItembutton()
    {
        activeItem.UseItem();
    }
}
