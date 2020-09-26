using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager instance;

    public string[] itemsHeld;
    public int[] numberOfItems;
    public Item[] referenceItems;

    public int currentGold = 50;

    // Start is called before the first frame update
    void Start()
    {

        // sort items at the start of the game, if you don't do this then items can take up empty slots when you buy them.
        SortItems();
    }

    private void Awake()
    {
        SetUpSingleton();
    }

    // Update is called once per frame
    void Update()
    {


    }

    // this is required to reference the items we have in our inventory to the database of possible items and return what data it should have.
    public Item GetItemDetails(string itemToGrab)
    {
        for (int i = 0; i < referenceItems.Length; i++)
        {
            if (referenceItems[i].itemName == itemToGrab)
            {
                return referenceItems[i];
            }
        }
        return null;
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

    public void SortItems()
    {
        // Variable to keep track of what's the next empty slot
        int emptySlot = 0;

        // Iterate through each inventory slot that we have
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            // If we see an available item in the inventory, move it to an empty slot
            if (itemsHeld[i] != "")
            {
                itemsHeld[emptySlot] = itemsHeld[i];
                numberOfItems[emptySlot] = numberOfItems[i];
                emptySlot++;
            }
        }

        // Populate inventory with "empty" items
        // We start at the next empty slot, and just iterate through the remaining slots left
        for (int i = emptySlot; i < itemsHeld.Length; i++)
        {
            // Reset the values
            itemsHeld[i] = "";
            numberOfItems[i] = 0;
        }
    }

    // add item by the string name of the item
    public void AddItem(string itemToAdd)
    {
        int newItemPosition = 0;
        bool foundSpace = false;

        //Find an empty slot, or a slot with the same string name item in it already in ITEMSHELD (inventory).
        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == "" || itemsHeld[i] == itemToAdd)
            {

                //set the item position for the item to be placed
                newItemPosition = i;
                i = itemsHeld.Length;
                foundSpace = true;
            }
        }

        //if we found an empty slot 
        if (foundSpace)
        {

            // sense check to see if the string given is viable.
            bool itemExists = false;

            //runs though the whole REFERENCE (possible items) item list to check if the name is a real one.
            for (int i = 0; i < referenceItems.Length; i++)
            {
                if (referenceItems[i].itemName == itemToAdd)
                {
                    itemExists = true;

                    i = referenceItems.Length;
                }
            }

            //if the item name actually exists
            if (itemExists)
            {
                //add the string itemToAdd to the position in itemsHeld, and increase the numberOfItems array at that same position by 1.
                itemsHeld[newItemPosition] = itemToAdd;
                numberOfItems[newItemPosition]++;
            }
            else
            {
                Debug.Log(itemToAdd + "not found");
            }
        }
        GameMenu.instance.ShowItems();
    }

    public void RemoveItem(string itemToRemove)
    {
        bool foundItem = false;
        int itemPosition = 0;

        for (int i = 0; i < itemsHeld.Length; i++)
        {
            if (itemsHeld[i] == itemToRemove)
            {
                foundItem = true;
                itemPosition = i;

                i = itemsHeld.Length;
            }
        }

        if (foundItem)
        {
            numberOfItems[itemPosition]--;

            if (numberOfItems[itemPosition] <= 0)
            {
                itemsHeld[itemPosition] = "";
                GameMenu.instance.itemActionBox.SetActive(false);
            }

            GameMenu.instance.ShowItems();
        }
        else
        {
            Debug.Log(itemToRemove + "couldnt remove");
        }
    }

    public void GainGold(int gold)
    {
        currentGold += gold;
    }
}
