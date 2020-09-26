using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetStats : MonoBehaviour
{
    public static PetStats instance;


    [Header("Battle Stats")]
    public int currentRed;
    public int maxRed = 10;
    public int currentBlue;
    public int maxBlue;

    public float strength;
    public float speed;
    public float intellect;
    public float charm;

    public float attackPower;
    public float defencePower;
    public float attackSpeed;
    public float Evasion;



    [Header("Sleepiness")]
    public float currentSleepiness;
    public int maxSleepiness = 100;
    public float sleepinessRate;
    public float sleepLength;
    public float sleepModifier;
    [Header("Health")]
    public float currentHealth;
    public float maxHealth = 100;
    public float HealthModifier = 1f;
    [Header("Happiness stats")]
    public float currentHapiness;
    public float maxHapiness = 100;
    public float HapinessModifier = 1f;
    public float BoredomRate = 1f;
    [Header("Hunger stats")]
    public float currentHunger;
    public float maxHunger = 100;
    public float HungerModifier = 1f;
    public float HungerRate;


    [Header("XP stats")]
    public int petLevel = 1;
    public int maxLevel = 100;





    private void Awake()
    {
        SetUpSingleton();
    }
    private void SetUpSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    /* public void SetPetStatsToDefault()
     {
         maxRed = maxRedDefault;
         maxBlue = maxBlueDefault;
         strength = strengthDefault;
         grit = gritDefault;
         speed = speedDefault;
         intellect = intellectDefault;
         charm = charmDefault;
     }*/




    public void setSleepPattern()
    {
        sleepinessRate = SleepingPatterns.sleepinessRate;
        sleepLength = SleepingPatterns.sleepLength;
        sleepModifier = SleepingPatterns.sleepModifier;
    }

    public PetSelect pets;
    public void setPetStatPotentials()
    {
        switch (pets)
        {
            case PetSelect.Chick:
                SleepingPatterns.ChickSleepPattern();
                break;
            case PetSelect.Cat:
                SleepingPatterns.CatSleepPattern();
                break;
            case PetSelect.Wolf:
                SleepingPatterns.WolfSleepPattern();
                break;
            case PetSelect.Lizard:
                SleepingPatterns.LizardSleepPattern();
                break;
            default:
                break;
        }
        setSleepPattern();
    }

    public void Update()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GettingSleepy", 0.01f, 0.2f);
        InvokeRepeating("GettingHungry", 0.01f, 0.2f);
        InvokeRepeating("GettingBored", 0.01f, 0.2f);
    }


    public void FillRedAndBlue()
    {
        currentBlue = maxBlue;
        currentRed = maxRed;
    }


    public void GainHealth(float HealthToGain)
    {
        currentHealth += HealthToGain;
        if (currentHealth >= maxHealth)
        {
            Debug.Log("Health over max");
            currentHealth = maxHealth;
        }
    }

    public void GainSleepiness(float sleepinessToGain)
    {
        currentSleepiness += sleepinessToGain;

        if (currentSleepiness >= 100)
        {
            Debug.Log("sleepinessover max");
            currentSleepiness = 100;
        }

        SleepinessSlider.instance.updateSleepinessSlider();

    }

    public void GainHapiness(float HapinessToGain)
    {
        currentHapiness += HapinessToGain;

        if (currentHapiness >= 100)
        {
            Debug.Log("Hapiness over max");
            currentHapiness = 100;
        }
        HapinessSlider.instance.updateHapinessSlider();

    }

    public void GainHunger(float HungerToGain)
    {
        currentHunger += HungerToGain;

        if (currentHunger >= 100)
        {
            Debug.Log("Hunger over max");
            currentHunger = 100;
        }
        HungerSlider.instance.updateHungerSlider();

    }

    public bool ResetSleepCycleOnceONLY = false;

    public void GettingSleepy()
    {
        if (Clock.instance.shouldTimePass)
        {

            currentSleepiness += Clock.instance.invokeTime20MS * sleepinessRate;

            if (currentSleepiness < 80 && ResetSleepCycleOnceONLY == true)
            {
                PetEmotes.instance.removeEmoteFromCycle(0);
                ResetSleepCycleOnceONLY = false;
            }

            if (currentSleepiness >= maxSleepiness)
            {
                currentSleepiness = maxSleepiness;
            }

            if (currentSleepiness > 80 && ResetSleepCycleOnceONLY == false)
            {
                PetEmotes.instance.addEmoteToCycle(0);
                PetEmotes.instance.resetCycle = true;
                ResetSleepCycleOnceONLY = true;
            }

            if (currentSleepiness >= 100)
            {
                Clock.instance.StopStartTime(false);

            }
            SleepinessSlider.instance.updateSleepinessSlider();
        }
    }
    // this is used due to gettinhungry being called in update but i only 
    public bool ResetHungerCycleOnceONLY = false;


    public void GettingHungry()
    {
        if (Clock.instance.shouldTimePass)
        {

            currentHunger += Clock.instance.invokeTime20MS * HungerRate;
            // Debug.Log("current hunger : " + currentHunger + "currentTimeHunger :" + currentTimeHunger);
        }


        if (currentHunger < 80 && ResetHungerCycleOnceONLY == true)
        {
            PetEmotes.instance.removeEmoteFromCycle(1);
            ResetHungerCycleOnceONLY = false;
        }

        if (currentHunger > 80 && ResetHungerCycleOnceONLY == false)
        {
            PetEmotes.instance.addEmoteToCycle(1);
            PetEmotes.instance.resetCycle = true;
            ResetHungerCycleOnceONLY = true;


        }

        if (currentHunger >= 100)
        {
            currentHunger = maxHunger;

        }
        HungerSlider.instance.updateHungerSlider();

    }

    public bool ResetBoredomCycleOnceONLY = false;
    public void GettingBored()
    {
        if (Clock.instance.shouldTimePass)
        {
            currentHapiness += Clock.instance.invokeTime20MS * BoredomRate;
        }


        if (currentHapiness < 80 && ResetBoredomCycleOnceONLY == true)
        {
            PetEmotes.instance.removeEmoteFromCycle(2);
            ResetBoredomCycleOnceONLY = false;
        }

        if (currentHapiness > 80 && ResetBoredomCycleOnceONLY == false)
        {
            PetEmotes.instance.addEmoteToCycle(2);
            PetEmotes.instance.resetCycle = true;
            ResetBoredomCycleOnceONLY = true;


        }

        if (currentHapiness >= 100)
        {
            currentHapiness = maxHapiness;

        }
        HapinessSlider.instance.updateHapinessSlider();
    }

    //gain stats methods to be called from other scripts
    public void gainMaxRed(int maxRedToGain)
    {
        maxRed += maxRedToGain;

    }

    public void gainMaxBlue(int maxBlueToGain)
    {
        maxBlue += maxBlueToGain;

    }


}

