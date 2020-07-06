using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FatigueSlider : MonoBehaviour
{

    public static FatigueSlider instance;
    public float maxFatigue;
    public float currentFatigue;

    private void Awake()
    {
        SetUpSingleton();
    }
    private void Start()
    {
        maxFatigue = PetStats.instance.maxFatigue;
    }
    // Update is called once per frame


    public void updateFatigueSlider()
    {
        currentFatigue = PetStats.instance.currentFatigue;
        GetComponent<Slider>().value = currentFatigue;
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
