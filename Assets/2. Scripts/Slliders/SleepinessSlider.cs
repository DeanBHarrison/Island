using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepinessSlider : MonoBehaviour
{
    public float maxSleepiness;
    public float currentSleepiness;
    public static SleepinessSlider instance;
    private void Awake()
    {
        SetUpSingleton();
    }

    private void Start()
    {
        maxSleepiness = PetStats.instance.maxSleepiness;
    }
    // Update is called once per frame


    public void updateSleepinessSlider()
    {
        currentSleepiness = PetStats.instance.currentSleepiness;
        GetComponent<Slider>().value = currentSleepiness;
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

    }
}
