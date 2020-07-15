using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnNPC : MonoBehaviour
{
    [Header("material Glow")]
    //switch between on mouse over
    public Material standardMat;
    public Material GlowMat;
    public GameObject spotLight;

    [Header("chat bubble")]
    public string hoverChat;
    public Transform InnKeeperTransform;
    //just to avoid using getcomponent

    public SpriteRenderer theSpriterenderer;

    private void Start()
    {
        if (PlayerController.instance.canMove)
        {
            InvokeRepeating("Chatter", 1f, 15f);
        }
    }

    private void Chatter()
    {
        ChatBubble.Create(InnKeeperTransform, new Vector2(0, 1f), "howdy partner");
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            DialogueManager.instance.SetChatOption(0, 2);
        }
    }

}
