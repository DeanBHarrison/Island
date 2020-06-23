using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultPetStats : MonoBehaviour
{
    [Header("Battle Stats")]
    public static int maxRed;
    public static int maxBlue;
    public static float strength;
    public static float grit;
    public static float speed;
    public static float intellect;
    public static float charm;


    public static void ChickDefaultStats()
    {
        maxRed = 9;
        maxBlue = 11;
        strength = 3;
        grit = 5;
        speed = 4;
        intellect = 4;
        charm = 7;

    }

    public static void CatDefaultStats()
    {
        maxRed = 11;
        maxBlue = 9;
        strength = 4;
        grit = 4;
        speed = 6;
        intellect = 4;
        charm = 5;


    }

    public static void LizardDefaultStats()
    {
        maxRed = 8;
        maxBlue = 12;
        strength = 3;
        grit = 5;
        speed = 5;
        intellect = 7;
        charm = 5;

    }

    public static void WolfDefaultStats()
    {
        maxRed = 12;
        maxBlue = 8;
        strength = 6;
        grit = 5;
        speed = 5;
        intellect = 4;
        charm = 5;

    }

}
