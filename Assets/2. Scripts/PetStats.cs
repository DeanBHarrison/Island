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
    [Header("Hunger stats")]
    public float currentHunger;
    public float maxHunger = 100;
    public float HungerModifier = 1f;
    public float HungerRate;

    //needs  removing
    [Header("XP stats")]
    public int petLevel = 1;
    public float currentXP;
    public float[] xpToNextLevel;
    public int maxLevel = 100;
    public float baseEXP = 1000f;
   

    // battle stats to gain when leveling
    private  int maxRedToGainOnlvl;
    private int maxBlueToGainOnlvl;
    private float strengthToGainOnlvl;
    private float gritToGainOnlvl;
    private float speedToGainOnlvl;
    private float intellectToGainOnlvl;
    private float charmToGainOnlvl;

    // default stats to give a pet
   /* private int maxRedDefault;
    private int maxBlueDefault;
    private float strengthDefault;
    private float gritDefault;
    private float speedDefault;
    private float intellectDefault;
    private float charmDefault;*/

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


    public void setStatsToGainOnLVL()
    {
        maxRedToGainOnlvl = PetStatsGainOnLevelUp.maxRedToGain;
        maxBlueToGainOnlvl = PetStatsGainOnLevelUp.maxBlueToGain;
        strengthToGainOnlvl = PetStatsGainOnLevelUp.strengthToGain;
        gritToGainOnlvl = PetStatsGainOnLevelUp.gritToGain;
        speedToGainOnlvl = PetStatsGainOnLevelUp.speedToGain;
        intellectToGainOnlvl = PetStatsGainOnLevelUp.intellectToGain;
        charmToGainOnlvl = PetStatsGainOnLevelUp.charmToGain;
    }

    public void setSleepPattern()
    {
      sleepinessRate = SleepingPatterns.sleepinessRate;
      sleepLength = SleepingPatterns.sleepLength;
      sleepModifier = SleepingPatterns.sleepModifier;
     }


    public void setPetStatPotentials()
    {
        switch (PetSpawner.instance.petIndex)
        {
            case 0:
                PetStatsGainOnLevelUp.ChickStatGain();
                SleepingPatterns.ChickSleepPattern();
                break;

            case 1:
                PetStatsGainOnLevelUp.CatStatGain();
                SleepingPatterns.CatSleepPattern();
                break;

            case 2:
                PetStatsGainOnLevelUp.WolfStatGain();
                SleepingPatterns.WolfSleepPattern();
                break;

            case 3:
                PetStatsGainOnLevelUp.LizardStatGain();
                SleepingPatterns.LizardSleepPattern();
                break;

            default:
                Debug.Log("something messed up with set stat pet potentials");
                break;
        }
        setSleepPattern();
        setStatsToGainOnLVL();
    }



    // Start is called before the first frame update
    void Start()
    {       
        SetUpXPRequirements();
       setPetStatPotentials();
        InvokeRepeating("GettingSleepy", 0.01f, 0.2f);
        InvokeRepeating("GettingHungry", 0.01f, 0.2f);
    }



    public void SetUpXPRequirements()
    {
        xpToNextLevel = new float[maxLevel];
        xpToNextLevel[0] = baseEXP;
        for (int i = 1; i < xpToNextLevel.Length; i++)
        {
            xpToNextLevel[i] = xpToNextLevel[i - 1] * 1.05f;
        }
    }

    public void GainXP(float xpToGain)
    {
        currentXP += xpToGain;
        if(currentXP >= xpToNextLevel[petLevel])
        {
            PetLevelUP();
        }
    }

    public void PetLevelUP()
    {
        currentXP -= xpToNextLevel[petLevel];
        petLevel++;
        maxRed += maxRedToGainOnlvl;
        maxBlue += maxBlueToGainOnlvl;
        strength += strengthToGainOnlvl;
        speed += speedToGainOnlvl;
        intellect += intellectToGainOnlvl;
        charm += charmToGainOnlvl;
        FillRedAndBlue();
        MusicPlayer.instance.PlaySFX(1);
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
            //DISABLED WHILE WORKING ON GAME
            // currentSleepiness += Time.deltaTime / Clock.instance.realSecondsPerDay * sleepinessRate;

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
                Clock.instance.shouldTimePass = false;

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
            //DISABLED WHILE WORKING ON GAME
            //currentHunger += Time.deltaTime / Clock.instance.realSecondsPerDay * HungerRate;

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

    //gain stats methods to be called from other scripts
    public void gainMaxRed(int maxRedToGain)
    {
        maxRed += maxRedToGain;
       
    }

    public void gainMaxBlue(int maxBlueToGain)
    {
        maxBlue += maxBlueToGain;

    }

    public void gainStrength(float strengthToGain)
    {
        strength += strengthToGain;
    }



    public void gainSpeed(float speedToGain)
    {
        speed += speedToGain;
    }

    public void gainIntellect(float intellectToGain)
    {
        intellect += intellectToGain;
    }

    public void gainCharm(float charmToGain)
    {
        charm += charmToGain;
    }

  
  
}

