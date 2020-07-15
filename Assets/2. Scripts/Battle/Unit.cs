using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    //class keeps track of the stats of the player and enemy as the fight goes on, is attached to the player and enemy and accessed though
    // function paramters in other methods.
    [Header("UI stats")]
    public string unitName;
    public int unitLevel;

    [Header("Combat stats")]
    public int damage;
    public int maxHP;
    public int currentHP;

    [Header("Energy resource")]
    public int currentEnergy;
    public int maxEnergy;

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
        
    }

    public bool SpendEnergy(int Energy)
    {
        currentEnergy -= Energy;

            if (currentEnergy <= -1)
            return true;
        else
            return false;
    }

    public void GainEnergy(int Energy)
    {
        currentEnergy += Energy;

        if (currentEnergy >= maxEnergy)
            currentEnergy = maxEnergy;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }
}
