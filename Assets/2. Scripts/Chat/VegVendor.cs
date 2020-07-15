using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VegVendor : MonoBehaviour
{
    public static VegVendor instance;
    public string[] VegVendorItemsForSale = new string[54];


    [Header("Glow")]
    //switch between on mouse over
    public Material standardMat;
    public Material GlowMat;
    //just to avoid using getcomponent when changing sprite renderer material
    public SpriteRenderer theSpriterenderer;
    public GameObject spotLight;

    public Transform VegVendorTransform;

    private void Start()
    {
        InvokeRepeating("Chatter", 4f, 20f);
    }

    private void Chatter()
    {
        if (PlayerController.instance.canMove)
        {
            ChatBubble.Create(VegVendorTransform, new Vector2(0, 1f), "Fresh Veggies for sale!");
        }
    }



    private void OnMouseEnter()
    {
        theSpriterenderer.material = GlowMat;
        spotLight.SetActive(true);
    }

    private void OnMouseExit()
    {
        theSpriterenderer.material = standardMat;
        spotLight.SetActive(false);
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
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            DialogueManager.instance.SetChatOption(1, 2);

        }
    }
    public void VegVendorSetupShop()
    {
        //feed the items for sale in from the vegvendor object, then set up items
        Shop.instance.SetItemsForSale(VegVendorItemsForSale);
        Shop.instance.SetUpItems();


        // these 2 are reset just so they arent already up when you click back into the shop
        Shop.instance.itemActionBox.SetActive(false);
        Shop.instance.itemDescriptionBox.SetActive(false);
    }

}
