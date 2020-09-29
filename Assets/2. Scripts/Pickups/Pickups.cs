using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{

    public bool isPlayerNear;
    public bool mouseEnter;
    public int soundToPlay;
    public Transform CoinToSpawn;
    public string pickupName;

    public Material standardMat;
    public Material GlowMat;
    public SpriteRenderer theSpriterenderer;
    // on triggers and mouse enter
    #region 
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerNear = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerNear = false;
        }
    }

    private void OnMouseEnter()
    {
        theSpriterenderer.material = GlowMat;
        mouseEnter = true;
    }

    private void OnMouseExit()
    {
        theSpriterenderer.material = standardMat;
        mouseEnter = false;
    }
    #endregion 

    public void PickUpItem()
    {
        if (isPlayerNear && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove && mouseEnter)
        {
           
            var go = Instantiate(CoinToSpawn, GameMenu.instance.transform);
            go.transform.position = Input.mousePosition;


            QuestManager.instance.CheckIfItemNeededForQuest(this);
            Destroy(gameObject);
            isPlayerNear = false;
            MusicPlayer.instance.PlaySFX(soundToPlay);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
