using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingPatterns : MonoBehaviour
{
    public static float sleepinessRate;
    public static float sleepLength;
    public static float sleepModifier;


    public static void ChickSleepPattern()
    {
        sleepinessRate = 100f;
        sleepLength = 6f;
        sleepModifier = 1f;

    }

    public static void CatSleepPattern()
    {
        sleepinessRate = 160f;
        sleepLength = 4f;
        sleepModifier = 1.1f;

    }

    public static void LizardSleepPattern()
    {
        sleepinessRate = 120f;
        sleepLength = 6;
        sleepModifier = 0.9f;

    }

    public static void WolfSleepPattern()
    {
        sleepinessRate = 100f;
        sleepLength = 7f;
        sleepModifier = 0.9f;

    }
}
