using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treadmill : MonoBehaviour
{



 
    public int soundToPlay;
    private bool isPlayerNear = false;
    public float fatigueToAdd;
    public float sleepinessToAdd;
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isPlayerNear = false;
        }
    }

    public void useTreadmill()
    {
        if (Input.GetButtonDown("Fire1") && isPlayerNear && PlayerController.instance.canMove)
        {
            PetStats.instance.gainSpeed(3);
            PetStats.instance.GainFatigue(fatigueToAdd);
            FatigueSlider.instance.updateFatigueSlider();
            if (PetStats.instance.currentFatigue >= 100)
            {
                PetStats.instance.GainSickness(5f);
                SicknessSlider.instance.updateSicknessSlider();
                Debug.Log("sickness added on use treamill");
            }           
            PetStats.instance.GainSleepiness(sleepinessToAdd);
            Clock.instance.PassTime(1);


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
