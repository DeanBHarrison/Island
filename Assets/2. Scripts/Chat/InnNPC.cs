using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnNPC : MonoBehaviour
{
 

    public void sleep()
    {
            Clock.instance.PassTime(PetStats.instance.sleepLength);
            PetStats.instance.currentSleepiness = 0;
            PetStats.instance.currentFatigue = 0;
            FatigueSlider.instance.updateFatigueSlider();
            DialogueManager.instance.DisplayNextSentence();
            Clock.instance.CheckIfItShouldBeDark();
            Clock.instance.shouldTimePass = true;
    }

}
