using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Type")]
    public bool isItem;
    public bool isFood;

    [Header("Item Details")]
    public string itemName;
    public string description;
    public int value;
    public Sprite itemSprite;


    [Header("Item Use amount")]
    public int amountToChange;
    public bool affectRed, affectBlue, affectFatigue, affectTiredness;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //heals pets  HP then discards 1 of the items
    public void UseItem()
    {
        Debug.Log("item used");
        if(isFood)
        {
            if(affectRed)
            {
                PetStats.instance.currentHunger += amountToChange;
                if(PetStats.instance.currentHunger < 0)
                {
                    PetStats.instance.currentHunger = 0;
                }
            }
        }

        InventoryManager.instance.RemoveItem(itemName);
 
    }
}
