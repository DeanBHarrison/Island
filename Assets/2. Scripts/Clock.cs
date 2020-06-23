using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public static Clock instance;
    public GameObject blackCanvas;
    public GameObject ClockHand;
    public bool shouldTimePass = false;
    

    public float realSecondsPerDay = 5f;
    private float rotationDegreesPerDay = 360f;
    public float dayLength;
    public float dayNormalisedForLightCycles;
    public static float dayNormalised;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {       
        TimePassing();
        DuskAndDawn();

        if(Input.GetKeyDown(KeyCode.X))
        {
            PassTime(0.2f);
            Debug.Log("time added");
        }
    }

    public void TimePassing()
    {
        
        if (shouldTimePass)
        {
            dayLength += Time.deltaTime / realSecondsPerDay;
            dayNormalised = dayLength % 1f;
            dayNormalisedForLightCycles = dayLength % 1f;
            ClockHand.transform.eulerAngles = new Vector3(0, 0, -dayNormalisedForLightCycles * rotationDegreesPerDay * 2f);
        }
    }

    public void DuskAndDawn()
    {
        if(dayNormalisedForLightCycles >= 0.4375 && dayNormalisedForLightCycles <= 0.5625)
        {
            var Image = blackCanvas.GetComponent<Image>();
            var tempColor = Image.color;
            tempColor.a = 4 * (dayNormalisedForLightCycles - 0.4375f);
            Image.color = tempColor;
        }
        else if(dayNormalisedForLightCycles >= 0.9375 || dayNormalisedForLightCycles <= 0.0625)
        {
            if(dayNormalisedForLightCycles > 0 && dayNormalisedForLightCycles < 0.0625)
            {
                dayNormalisedForLightCycles = dayNormalisedForLightCycles + 1f;
            }
            var Image = blackCanvas.GetComponent<Image>();
            var tempColor = Image.color;
            tempColor.a = 0.5f  - 4*(dayNormalisedForLightCycles - 0.9375f);
            Image.color = tempColor;
        }

    }

    public void PassTime(float timeToPass)
    {
        shouldTimePass = false;
        Debug.Log("time added");
        dayLength += timeToPass;
        shouldTimePass = true;
    }
}
