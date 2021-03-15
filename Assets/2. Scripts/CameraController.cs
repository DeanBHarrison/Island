using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public int MusicToPlay;
    public Transform target;
    public bool disablePlayerFollow = false;

    public Tilemap theMap;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    private float halfHeight;
    private float halfWidth;

    private void Awake()
    {
        setUpSingleton();
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
                Debug.Log("there is now another instance of the cameracontroller script");
                Destroy(gameObject);
                Debug.Log("copy destroyed");
            }
        }
    }

    [Header("changing fade")]
    // gets the global light 2d component, can adjust intensity on this to make it light/dark
    public Light2D light2d;

    // Start is called before the first frame update
    void Start()
    {
        setUpCameraLimits();
        MusicPlayer.instance.PlayMusic(MusicToPlay);
        InvokeRepeating("sunset", 0.01f, 0.05f);
        InvokeRepeating("sunrise", 0.01f, 0.05f);


    }
    public void sunset()
    {
        Clock.instance.Sunset();
        if (Clock.instance.TimeOfDayAdjustedForSunset > Clock.instance.sunset && Clock.instance.TimeOfDayAdjustedForSunset < (Clock.instance.sunset + Clock.instance.sunsetDuration))
        {

            
            // light2d.intensity = Clock.instance.Sunset();
            Debug.Log("sunset");
        }
    }

    public void sunrise()
    {
        Clock.instance.Sunrise();
        if (Clock.instance.TimeOfDayAdjustedForSunrise > Clock.instance.sunrise && Clock.instance.TimeOfDayAdjustedForSunrise < (Clock.instance.sunrise + Clock.instance.sunriseDuration))
        {
            //light2d.intensity = Clock.instance.Sunrise();
            // Debug.Log("sunrise");
        }
    }
    void LateUpdate()
    {
        CameraFollowPlayer();
        SetCameraBounds();
    }

    public void setUpCameraLimits()
    {
        if (!disablePlayerFollow)
        {
            target = PlayerController.instance.transform;
            halfHeight = Camera.main.orthographicSize;
            halfWidth = halfHeight * Camera.main.aspect;
            bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
            topRightLimit = theMap.localBounds.max - new Vector3(halfWidth, halfHeight, 0f);
            PlayerController.instance.Setbounds(theMap.localBounds.min, theMap.localBounds.max);
        }
    }

    //sets the camera to follow the player
    public void CameraFollowPlayer()
    {
        if (!disablePlayerFollow)
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }

    //stops the camera being able to go outside of the level bounds
    public void SetCameraBounds()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x),
                                         Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);

    }




    /* public void Sunset()
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
             tempColor.a = (TimeOfDayAdjustedForSunset - sunset) / (2 * sunsetDuration);
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
             if (timeOfDay - sunrise > timeOfDay - sunset)
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

         if (sunrise < timeOfDay && sunset > timeOfDay)
         {
             ItShouldBeDark = false;
             // Debug.Log(ItShouldBeDark + "3");
         }

         if (sunrise > timeOfDay && sunset < timeOfDay)
         {
             ItShouldBeDark = true;
             //  Debug.Log(ItShouldBeDark + "4");
         }

         if (ItShouldBeDark)
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
     }*/
}
