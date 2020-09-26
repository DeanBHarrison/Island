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
    public int Power;
    public int currentResolve;
    public int maxResolve;
    public int HitsPerRound;
    public int Grit;

    [Header("Energy resource")]
    public int currentEnergy;
    public int maxEnergy;

    [Header("Enemy Combos")]
    public int numberOfCombos;
    public List<string> Combo1;
    public List<string> Combo2;
    public List<string> Combo3;
    public List<string> Combo4;
    public List<string> Combo5;
    public List<string> Combo6;
    public List<string> Combo7;
    public List<string> Combo8;
    public List<string> Combo9;
    public List<string> Combo10;



    public bool TakeDamage(int dmg)
    {
        currentResolve -= (dmg - Grit);

        if (currentResolve <= 0)
            return true;
        else
            return false;
        
    }

    public bool SpendEnergy(int Energy)
    {
        

        if ((currentEnergy - Energy) <= -1)
        {
            return true;
        }
        else
        {
            currentEnergy -= Energy;
            return false;
        }
            
    }

    public void GainEnergy(int Energy)
    {
        currentEnergy += Energy;

        if (currentEnergy >= maxEnergy)
            currentEnergy = maxEnergy;
    }

    public void Heal(int amount)
    {
        currentResolve += amount;
        if (currentResolve > maxResolve)
            currentResolve = maxResolve;
    }
}
