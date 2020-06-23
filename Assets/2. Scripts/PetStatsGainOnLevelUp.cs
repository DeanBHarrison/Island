using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetStatsGainOnLevelUp : MonoBehaviour
{
    public static int maxRedToGain;
    public static int maxBlueToGain;
    public static float strengthToGain;
    public static float gritToGain;
    public static float speedToGain;
    public static float intellectToGain;
    public static float charmToGain;


    public static void ChickStatGain()
    {
        maxRedToGain = 9;
        maxBlueToGain = 11;
        strengthToGain = 0.5f;
        gritToGain = 1.2f;
        speedToGain = .9f;
        intellectToGain = .9f;
        charmToGain = .5f;
    }

    public static void CatStatGain()
    {
        maxRedToGain = 10;
        maxBlueToGain = 9;
        strengthToGain = 0.6f;
        gritToGain = .9f;
        speedToGain = 1.2f;
        intellectToGain = 1.1f;
        charmToGain = 1.1f;
    }

    public static void LizardStatGain()
    {
        maxRedToGain = 8;
        maxBlueToGain = 12;
        strengthToGain = 0.4f;
        gritToGain = 1f;
        speedToGain = 1.1f;
        intellectToGain = 2f;
        charmToGain = 1f;
    }

    public static void WolfStatGain()
    {
        maxRedToGain = 10;
        maxBlueToGain = 10;
        strengthToGain = 1.3f;
        gritToGain = 1.4f;
        speedToGain = 1.3f;
        intellectToGain = .9f;
        charmToGain = 1f;
    }


}
