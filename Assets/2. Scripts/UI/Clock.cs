using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public static Clock instance;
    public GameObject blackCanvas;
    public GameObject ClockHand;
    public Image background;
    public bool shouldTimePass = false;


    public bool nightTime;
    public Color dayTimeClock;
    public Color nightTimeClock;
    

    //how long 1 day rotation will take in real seconds
    public float realSecondsPerDay = 5f;
    // needed only to rotate the hand on the clock
    private float rotationDegreesPerDay = 360f;
    // daylength starts at 0 and gets bigger by 1 every day
    public float dayLength;
    // this is daylength % 1, meaning its 0 to 1. 0 being the start of the day 1 being the end.
    private float dayNormalised;
    // this is dayNoramlised * 24 to get time in 24 hours.
    public float timeOfDay;

    public float invokeTime20MS;
    public float lastInvokeTime20MS = 0;

    public float sunset = 21;
    public float sunsetDuration = 3;
    private float TimeOfDayAdjustedForSunset;

    public float sunrise = 9;
    public float sunriseDuration = 3;
    private float TimeOfDayAdjustedForSunrise;

    public bool ItShouldBeDark;


    private void Awake()
    {
        setUpSingleton();
        InvokeRepeating("TimeForInvokeRepeating", 0.01f, 0.2f);

    }

    private void Start()
    {
        CheckIfItShouldBeDark();
        InvokeRepeating("Sunset", 0.01f, 0.2f);
        InvokeRepeating("Sunrise", 0.01f, 0.2f);
        InvokeRepeating("ChangeClockColor", 0.01f, 1f);
    }

    public void setUpSingleton()
    {
        if (instance == null)
        {
            // this is the script that is attached to the gameobject, instance is the variable.
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Debug.Log("there is now another instance of the clock script");
                Destroy(gameObject);
                Debug.Log("copy destroyed");
            }
        }
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {       
        TimePassing();      
    }

    private void TimeForInvokeRepeating()
    {
        invokeTime20MS = dayLength - lastInvokeTime20MS;
        lastInvokeTime20MS = dayLength;
    }


    public void TimePassing()
    {
        timeOfDay = dayNormalised * 24;
        if (shouldTimePass)
        {
            // if you call time.delta every frame you are essentially adding 1 per second, with real seconds at 24 its adding 1/24th of a second per second.
            dayLength += Time.deltaTime / realSecondsPerDay;
            dayNormalised = dayLength % 1f;
            ClockHand.transform.eulerAngles = new Vector3(0, 0, -dayNormalised * rotationDegreesPerDay * 2f);
        }
    }

    public void Sunset()
    {
        // set variblaes to be used in method below
        var Image = blackCanvas.GetComponent<Image>();
        TimeOfDayAdjustedForSunset = timeOfDay;

        //This makes the time of day counter not reset past 12 if it needs to keep going to scale the alpha down for sunset. it keeps going
        // until sunset time when it goes back to sunset amount
        if ((sunset + sunsetDuration > 24))
        {
            if (TimeOfDayAdjustedForSunset < sunset)
            {
                TimeOfDayAdjustedForSunset += 24f;
            }
        }

        if (TimeOfDayAdjustedForSunset > sunset && TimeOfDayAdjustedForSunset < (sunset + sunsetDuration))
        {
           
            var tempColor = Image.color;
            tempColor.a = (TimeOfDayAdjustedForSunset - sunset)/ (2*sunsetDuration);
            Image.color = tempColor;

        }
    }

    public void Sunrise()
    {
        // set variblaes to be used in method below
        var Image = blackCanvas.GetComponent<Image>();
        TimeOfDayAdjustedForSunrise = timeOfDay;

        //This makes the time of day counter not reset past 12 if it needs to keep going to scale the alpha down for sunset. it keeps going
        // until sunset time when it goes back to sunset amount
        if ((sunrise + sunriseDuration > 24))
        {
            Debug.Log("check");
            if (TimeOfDayAdjustedForSunrise < sunrise)
            {
                Debug.Log("time of day being adjusted sunrise");
                TimeOfDayAdjustedForSunrise += 24f;
            }
        }

        if (TimeOfDayAdjustedForSunrise > sunrise && TimeOfDayAdjustedForSunrise < (sunrise + sunriseDuration))
        {
            var tempColor = Image.color;
            tempColor.a = 0.5F - (TimeOfDayAdjustedForSunrise - sunrise) / (2 * sunriseDuration);
            Image.color = tempColor;

        }
    }

    public void CheckIfItShouldBeDark()
    {

        
        var Image = blackCanvas.GetComponent<Image>();
        if (sunrise > timeOfDay && sunset > timeOfDay)
        {
            if(timeOfDay - sunrise > timeOfDay - sunset)
            {
                ItShouldBeDark = true;
                // Debug.Log(ItShouldBeDark + "1");
            }
        }

        if (sunrise < timeOfDay && sunset < timeOfDay)
        {
            if (timeOfDay - sunrise > timeOfDay - sunset)
            {
                ItShouldBeDark = false;
                // Debug.Log(ItShouldBeDark + "2");
            }
        }

        if(sunrise < timeOfDay && sunset > timeOfDay)
        {         
                ItShouldBeDark = false;
            // Debug.Log(ItShouldBeDark + "3");
        }

        if (sunrise > timeOfDay && sunset < timeOfDay)
        {
            ItShouldBeDark = true;
            //  Debug.Log(ItShouldBeDark + "4");
        }

        if(ItShouldBeDark)
        {
            var tempColor = Image.color;
            tempColor.a = 0.5F;
            Image.color = tempColor;
            //particleController.instance.WhichParticleSystem(true);
        }
        else
        {
            var tempColor = Image.color;
            tempColor.a = 0F;
            Image.color = tempColor;
            //particleController.instance.WhichParticleSystem(false);
        }
       // Debug.Log(ItShouldBeDark);
    }

    public void ChangeClockColor()
    {
        if(dayNormalised <=0.5)
        {
            dayTimeClock.a = 1;
            background.color = dayTimeClock;
            
        }
        else
        {
            nightTimeClock.a = 1;
            background.color = nightTimeClock;
        }
    }

    public void PassTime(float timeToPass)
    {
        shouldTimePass = false;
        dayLength += timeToPass/24;
        // to fix sleepiness filling up even when set to 0 after sleeping
        lastInvokeTime20MS = dayLength;
        shouldTimePass = true;
    }
}
