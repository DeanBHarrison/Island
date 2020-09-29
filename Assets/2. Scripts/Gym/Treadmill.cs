using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{



 
    public int soundToPlay;
    private bool isPlayerNear = false;
    public float fatigueToAdd;
    public float sleepinessToAdd;
    public bool mouseEnter = false;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        useTreadmill();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerNear = true;
        }
    }

    private void OnMouseEnter()
    {
        mouseEnter = true;
    }

    private void OnMouseExit()
    {
        mouseEnter = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerNear = false;
        }
    }

    public void useTreadmill()
    {
        if (Input.GetButtonDown("Fire1") && isPlayerNear && !PlayerController.instance.isWaiting && mouseEnter)
        {         
            PetStats.instance.GainSleepiness(sleepinessToAdd);

            // when i pass time its not instantly updating the color the background should be.
            // let's speed time up for 1 hour instead of skipping an hour.
            if (!PlayerController.instance.isWaiting)
            {
                Clock.instance.SpeedupTime(6, 2);
            }


            var go = Instantiate(GameAssets.i.GreenStatCoin, GameMenu.instance.transform);
            go.transform.position = Input.mousePosition;
            MusicPlayer.instance.PlaySFX(soundToPlay);           
            Debug.Log("trained on treamill");
        }
    }

    /*maxRed += maxRedToGainOnlvl;
     maxBlue += maxBlueToGainOnlvl;
     strength += strengthToGainOnlvl;
     grit += gritToGainOnlvl;
     speed += speedToGainOnlvl;
     intellect += intellectToGainOnlvl;
     charm += charmToGainOnlvl;
 */
}
