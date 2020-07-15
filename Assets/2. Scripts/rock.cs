using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock : MonoBehaviour
{
    public int xpToGain;
    public bool isPlayerNear;
    public int soundToPlay;
    public float fatigueToAdd;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerNear = false;
        }
    }

    private void Update()
    {
        PickUpRock();
    }  


    public void PickUpRock()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.R) && PlayerController.instance.canMove)
        {
            PetStats.instance.GainXP(xpToGain);
            Destroy(gameObject);
            isPlayerNear = false;
            MusicPlayer.instance.PlaySFX(soundToPlay);
        }
    }

}


