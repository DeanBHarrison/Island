using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemButton : MonoBehaviour
{
    public Image buttonImage;
    public Text amountText;
    public int buttonValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
       
        if (GameMenu.instance.menuIsActive)
        {
            if (InventoryManager.instance.itemsHeld[buttonValue] != "")
            {
                GameMenu.instance.SelectItem(InventoryManager.instance.GetItemDetails(InventoryManager.instance.itemsHeld[buttonValue]));

                if (!GameMenu.instance.itemDescriptionBox.activeInHierarchy)
                {
                    GameMenu.instance.itemDescriptionBox.SetActive(true);
                    GameMenu.instance.itemDescriptionBox.transform.position = gameObject.transform.position + new Vector3(0f, -200f, 0f);
                }
                else
                {
                    GameMenu.instance.itemDescriptionBox.SetActive(false);
                }

                if (!GameMenu.instance.itemActionBox.activeInHierarchy)
                {
                    GameMenu.instance.itemActionBox.SetActive(true);
                    GameMenu.instance.itemActionBox.transform.position = gameObject.transform.position + new Vector3(150f, 40f, 0f);
                }
                else
                {
                    GameMenu.instance.itemActionBox.SetActive(false);
                }
            }
        }


    }

    public void pressShop()
    {
        Debug.Log(GameMenu.instance.menuIsActive);

        // set up conditions for when clicking an item in the shop
        if (Shop.instance.shopMenu.activeInHierarchy)
        {
            if (Shop.instance.itemsForSale[buttonValue] != "")
            {
                Shop.instance.selectBuyItem(InventoryManager.instance.GetItemDetails(Shop.instance.itemsForSale[buttonValue]));

                if (!Shop.instance.itemDescriptionBox.activeInHierarchy)
                {
                    Shop.instance.itemDescriptionBox.SetActive(true);
                    Shop.instance.itemDescriptionBox.transform.position = gameObject.transform.position + new Vector3(0f, -200f, 0f);
                }
                else
                {
                    Shop.instance.itemDescriptionBox.SetActive(false);
                }

                if (!Shop.instance.itemActionBox.activeInHierarchy)
                {
                    Shop.instance.itemActionBox.SetActive(true);
                    Shop.instance.itemActionBox.transform.position = gameObject.transform.position + new Vector3(150f, 40f, 0f);
                }
                else
                {
                    Shop.instance.itemActionBox.SetActive(false);
                }
            }
        }
      
    }


}
