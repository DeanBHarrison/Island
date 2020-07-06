using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegVendor : MonoBehaviour
{
    public string[] itemsForSale = new string[54];
    public GameObject shopMenu;
  public void openShop()
    {
        // set the window active
        shopMenu.SetActive(true);

        //feed the items for sale in from the vegvendor object, then set up items
        Shop.instance.SetItemsForSale(itemsForSale);
        Shop.instance.SetUpItems();

        // these 2 are reset just so they arent already up when you click back into the shop
        Shop.instance.itemActionBox.SetActive(false);
        Shop.instance.itemDescriptionBox.SetActive(false);

    }
}
