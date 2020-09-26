using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSheep : MonoBehaviour
{

    public bool playerNear = false;

    public Transform SheepTransform;
    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("Chatter", 5f, 9f);
    }



    private void Chatter()
    {
        if (PlayerController.instance.canMove)
        {
            ChatBubble.Create(SheepTransform, new Vector2(0, 1f), "Baa Baa motherfucker, come over here");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerNear = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && playerNear)
        {
            ObjectEnabler.instance.disableUI();
            SceneManager.LoadScene("Battle");
        }
    }
}
