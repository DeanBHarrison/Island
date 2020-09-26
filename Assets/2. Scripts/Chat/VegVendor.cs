using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegVendor : NPC
{
    public static VegVendor instance;
    

    private void Start()
    {
        InvokeRepeating("Chatter", 4f, 20f);
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Debug.Log("Veg vendor instance deleted");
                Destroy(gameObject);
            }
        }
    }
  

    public void VegVendorSetupShop()
    {
        //feed the items for sale in from the vegvendor object, then set up items
        Shop.instance.SetItemsForSale(itemsForSale);
        Shop.instance.SetUpItems();


        // these 2 are reset just so they arent already up when you click back into the shop
        Shop.instance.ActionDescriptionOnOff(false);

    }

}
