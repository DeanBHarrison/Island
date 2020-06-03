using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enables the file "techs" to be right clicked in unity, and an asset to be created on the right click menu. The menu is called "technique" and the default
// file name is "new tech"
[CreateAssetMenu(fileName = "New tech", menuName = "technique")]
public class Techs : ScriptableObject
{
    //setting the variables that you want to be created on every "new tech" asset that is right-click created in the unity menu, these variables can be set
    // on the "new tech" object that is created 
    public new string name;
    public string description;

    public Sprite artwork;

    public int manaCost;
    public int attack;

    public void PrintName()
    {
        Debug.Log(name + ": " + description + "The card costs: " + manaCost);
    }
}
