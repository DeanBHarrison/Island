using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    // set instance
    public static Shop instance;

    // shop window gameobject
    public GameObject shopMenu;
    public GameObject itemDescriptionBox;
    public GameObject itemActionBox;

    // script variable of the item we select from the shop, this gets run in a script that finds the details of the script via its name vs a reference item database.

    public Item selectedItem;
    public Text buyItemPrice, buyItemName, buyItemDescription;

    // need to update gold when opening
    public Text goldText;

    // need an array of items to populate the shop with
    public string[] itemsForSale;

    public itemButton[] buyItemButtons;

    private void Start()
    {
        
    }

    private void Awake()
    {
        instance = this;
    }

    public void SetUpItems()
    {
        goldText.text = InventoryManager.instance.currentGold.ToString() + "g";
        for (int i = 0; i < buyItemButtons.Length; i++)
        {
            // gives each of the 54 buttons a number index in it's buttonvalue field from 1 to 54
            buyItemButtons[i].buttonValue = i;

            // for every item that has a string name in the items SOLD array, set the image object to be active, set the sprite to what it should be,
            if (itemsForSale[i] != "")
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(true);
                // here we have to get the script information for the specific item from the reference item database, so we have to call a function that searches the database given the string of the item we have.
                buyItemButtons[i].buttonImage.sprite = InventoryManager.instance.GetItemDetails(itemsForSale[i]).itemSprite;
                buyItemButtons[i].amountText.text = "";
            }
            else
            {
                buyItemButtons[i].buttonImage.gameObject.SetActive(false);
                buyItemButtons[i].amountText.text = "";
            }
        }

    }
 
    public void SetItemsForSale(string [] itemsforsale)
    {
        itemsForSale = itemsforsale;
    }

    public void selectBuyItem(Item Itemtobuy)
    {
        selectedItem = Itemtobuy;
        buyItemName.text = selectedItem.itemName;
        buyItemDescription.text = selectedItem.description;
        buyItemPrice.text = selectedItem.value + "g";
    }

    public void closeActionButton()
    {
        itemActionBox.SetActive(false);
    }
    public void buyItem()
    {
        if(InventoryManager.instance.currentGold >= selectedItem.value)
        {
            InventoryManager.instance.currentGold -= selectedItem.value;
            InventoryManager.instance.AddItem(selectedItem.itemName);
               
        }

        goldText.text = InventoryManager.instance.currentGold.ToString() + "g";
        GameMenu.instance.SetCurrentGold();
    }

    

}
