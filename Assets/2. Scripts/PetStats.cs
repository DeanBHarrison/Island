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
    public float grit;
    public float speed;
    public float intellect;
    public float charm;
    public float attackPower;
    public float defencePower;

    [Header("Non-Battle Stats")]
    public float currentSleepiness;
    public int maxSleepiness = 100;
    public float sleepinessRate;
    public float sleepLength;
    public float sleepModifier;

    public float currentFatigue;
    public int maxFatigue = 100;

    //XP/level based stats
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
    private int maxRedDefault;
    private int maxBlueDefault;
    private float strengthDefault;
    private float gritDefault;
    private float speedDefault;
    private float intellectDefault;
    private float charmDefault;

    public void SetPetStatsToDefault()
    {
        maxRed = maxRedDefault;
        maxBlue = maxBlueDefault;
        strength = strengthDefault;
        grit = gritDefault;
        speed = speedDefault;
        intellect = intellectDefault;
        charm = charmDefault;
    }
    public void setDefaultStatsPotential()
    {
        maxRedDefault = DefaultPetStats.maxRed;
        maxBlueDefault = DefaultPetStats.maxBlue;
        strengthDefault = DefaultPetStats.strength;
        gritDefault = DefaultPetStats.grit;
        speedDefault = DefaultPetStats.speed;
        intellectDefault = DefaultPetStats.intellect;
        charmDefault = DefaultPetStats.charm;
    }

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
                DefaultPetStats.ChickDefaultStats();
                break;

            case 1:
                PetStatsGainOnLevelUp.CatStatGain();
                SleepingPatterns.CatSleepPattern();
                DefaultPetStats.CatDefaultStats();
                break;

            case 2:
                PetStatsGainOnLevelUp.WolfStatGain();
                SleepingPatterns.WolfSleepPattern();
                DefaultPetStats.WolfDefaultStats();
                break;

            case 3:
                PetStatsGainOnLevelUp.LizardStatGain();
                SleepingPatterns.LizardSleepPattern();
                DefaultPetStats.LizardDefaultStats();
                break;

            default:
                Debug.Log("something messed up with set stat pet potentials");
                break;
        }
        setSleepPattern();
        setStatsToGainOnLVL();
        setDefaultStatsPotential();
    }



    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        SetUpXPRequirements();
       setPetStatPotentials();
    }

    // Update is called once per frame
    void Update()
    {
        GettingSleepy();



        if (Input.GetKeyDown(KeyCode.K))
        {
            GainXP(500);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            GainFatigue(20);
        }


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
        grit += gritToGainOnlvl;
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

    public void GainFatigue(float fatigueToGain)
    {
        currentFatigue += Mathf.FloorToInt(fatigueToGain);
        if (currentFatigue >= maxFatigue)
        {
            Debug.Log("fatigue over max");
            currentFatigue = maxFatigue;
        }
        
    }

    public void GettingSleepy()
    {
        if (Clock.instance.shouldTimePass)
        {
            currentSleepiness += Time.deltaTime / Clock.instance.realSecondsPerDay * sleepinessRate;

            if (currentSleepiness >= maxSleepiness)
            {
                Debug.Log("sleepiness over max");
                currentSleepiness = maxSleepiness;
            }

            if (currentSleepiness > 80)
            {
                PlayerController.instance.canMove = false;
                Debug.Log("Need to sleep!");
                Clock.instance.shouldTimePass = false;
                GameMenu.instance.sleepButton.SetActive(true);
            }
        }
    }



  
  
}

