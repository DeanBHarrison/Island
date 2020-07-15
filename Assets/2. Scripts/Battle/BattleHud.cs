using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{

    public Text nameText;
    public Text levelText;
    public Slider hpSlider;
    public Slider EnergySlider;

    // Method to Set the information on the HUD
    // takes the "Unit" class as a paramater and then sets all the info based on that.
    public void SetHud(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = "Level " + unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        EnergySlider.maxValue = unit.maxEnergy;
        EnergySlider.value = unit.currentEnergy;
    }


    // this is called when damage is taken to update the HP slider.
    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    // this needs to be called when energy is used in order to update the slider.
    public void SetEnergy(int Energy)
    {
        EnergySlider.value = Energy;
    }
}
