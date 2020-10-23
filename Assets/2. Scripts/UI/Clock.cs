using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{

    [Header("changing fade")]





    [Header("clock")]
    public static Clock instance;
    public GameObject ClockHand;
    public Image background;
    public bool shouldTimePass = false;


    public bool nightTime;
    public Color dayTimeClock;
    public Color nightTimeClock;
    

    //how long 1 day rotation will take in real seconds
    public float realSecondsPerDay = 48f;
    // needed only to rotate the hand on the clock
    private float rotationDegreesPerDay = 360f;
    // daylength starts at 0 and gets bigger by 1 every day, this is the original variable that controls time.
    public float dayLength;
    // this is daylength % 1, meaning its 0 to 1. 0 being the start of the day 1 being the end.
    private float dayNormalised;
    // this is dayNoramlised * 24 to get time in 24 hours.
    public float timeOfDay;

    public float invokeTime20MS;
    public float lastInvokeTime20MS = 0;

    public float sunset = 21;
    public float sunsetDuration = 3;
    public float TimeOfDayAdjustedForSunset;

    public float sunrise = 9;
    public float sunriseDuration = 3;
    public float TimeOfDayAdjustedForSunrise;

    public bool ItShouldBeDark;


    private void Awake()
    {
        setUpSingleton();
        InvokeRepeating("TimeForInvokeRepeating", 0.01f, 0.2f);

    }

    public void StopStartTime(bool YesNoTime)
    {
        shouldTimePass = YesNoTime;
    }

    private void Start()
    {
        CheckIfItShouldBeDark();
        //InvokeRepeating("Sunset", 0.01f, 0.2f);
        //InvokeRepeating("Sunrise", 0.01f, 0.2f);
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

    // 23.5+24.5 
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
           // Debug.Log("time passing!!!" + shouldTimePass);
            // if you call time.delta every frame you are essentially adding 1 per second, with real seconds at 24 its adding 1/24th of a second per second.
            dayLength += Time.deltaTime / realSecondsPerDay;
            dayNormalised = dayLength % 1f;
            ClockHand.transform.eulerAngles = new Vector3(0, 0, -dayNormalised * rotationDegreesPerDay * 2f);
        }
    }

    public void TimePassOnce()
    {
        dayNormalised = dayLength % 1f;
        ClockHand.transform.eulerAngles = new Vector3(0, 0, -dayNormalised * rotationDegreesPerDay * 2f);
    }

    public float Sunset()
    {
        // set variblaes to be used in method below
        float Fadefloat = 0;
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

            //Fadefloat = (TimeOfDayAdjustedForSunset - sunset)/ (2*sunsetDuration);
            Fadefloat = 1F - (TimeOfDayAdjustedForSunset - sunset) / (2 * sunsetDuration);
            return Fadefloat;
        }
        return Fadefloat;
    }

    public float Sunrise()
    {
        // set variblaes to be used in method below
        float Fadefloat = 0;
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
            Fadefloat = 0.5f + (TimeOfDayAdjustedForSunrise - sunrise) / (2 * sunriseDuration);
            //Fadefloat = 0.5F - (TimeOfDayAdjustedForSunrise - sunrise) / (2 * sunriseDuration);
            return Fadefloat;
        }
        return Fadefloat;
    }

    public void CheckIfItShouldBeDark()
    {      
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
            CameraController.instance.light2d.intensity = 0.5f;
            //particleController.instance.WhichParticleSystem(true);
        }
        else
        {
            CameraController.instance.light2d.intensity = 1f;
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
        CheckIfItShouldBeDark();
        shouldTimePass = true;
    }




    public void SpeedupTime(int speed, int hourstopass)
    {
        StartCoroutine(Speedtime(speed, hourstopass));
    }
    IEnumerator Speedtime(int speed, int hourstopass)
    {
        PlayerController.instance.isWaiting = true;
        double time = timeOfDay;
        while (timeOfDay == time)
        {
            realSecondsPerDay /= speed;
            yield return new WaitForSeconds(realSecondsPerDay/24* hourstopass);
            realSecondsPerDay *= speed;
        }
        PlayerController.instance.isWaiting = false;

    }


}
