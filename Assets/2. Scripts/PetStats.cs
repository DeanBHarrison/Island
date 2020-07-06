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
    [Header("Fatigue")]
    public float currentFatigue;
    public float maxFatigue = 100;
    [Header("Sleep")]
    public float currentSleepiness;
    public int maxSleepiness = 100;
    public float sleepinessRate;
    public float sleepLength;
    public float sleepModifier;
    [Header("Sickness stats")]
    public float currentSickness;
    public float maxSickness = 100;
    public float sicknessModifier = 1f;
    [Header("Discipline stats")]
    public float currentDiscipline;
    public float maxDiscipline = 100;
    public float DisciplineModifier = 1f;
    [Header("Hunger stats")]
    public float currentHunger;
    public float maxHunger = 100;
    public float HungerModifier = 1f;
    public float HungerRate;
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
    private int maxRedDefault;
    private int maxBlueDefault;
    private float strengthDefault;
    private float gritDefault;
    private float speedDefault;
    private float intellectDefault;
    private float charmDefault;

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
        currentFatigue += fatigueToGain;
        if (currentFatigue >= maxFatigue)
        {
            Debug.Log("fatigue over max");
            currentFatigue = maxFatigue;
        }       
    }

    public void GainSickness(float sicknessToGain)
    {
        currentSickness += sicknessToGain;
        if (currentSickness >= maxSickness)
        {
            Debug.Log("sickness over max");
            currentSickness = maxSickness;
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

    public void GainDiscipline(float DisciplineToGain)
    {
        currentDiscipline += DisciplineToGain;

        if (currentDiscipline >= 100)
        {
            Debug.Log("Discipline over max");
            currentDiscipline = 100;
        }
        DisciplineSlider.instance.updateDisciplineSlider();

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
                Debug.Log("sleepiness over max");
                currentSleepiness = maxSleepiness;
            }

            if (currentSleepiness > 80 && ResetSleepCycleOnceONLY == false)
            {
                Debug.Log("sleep trigger");
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
            Debug.Log("hunger trigger");
                PetEmotes.instance.addEmoteToCycle(1);
                PetEmotes.instance.resetCycle = true;
                ResetHungerCycleOnceONLY = true;


            }

            if (currentHunger >= 100)
            {
                Debug.Log("hunger over  max");
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

    public void gainGrit(float gritToGain)
    {
        grit += gritToGain;
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

