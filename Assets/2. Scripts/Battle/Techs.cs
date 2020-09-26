using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Techs : MonoBehaviour
{

    public Sprite techSprite;

    public string TechName;
    public int EnergyCost;

    public bool doesDamage;
    public bool restoresHP;

    public int damageAmount;
    public int healingAmount;

    [TextArea(3, 10)]
    public string Description;
    
}
