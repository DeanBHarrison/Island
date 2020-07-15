using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HapinessSlider : MonoBehaviour
{
    public float maxHapiness;
    public float currentHapiness;
    public static HapinessSlider instance;
    private void Awake()
    {
        SetUpSingleton();
    }

    private void Start()
    {
        maxHapiness = PetStats.instance.maxHapiness;
    }
    // Update is called once per frame


    public void updateHapinessSlider()
    {
        currentHapiness = PetStats.instance.currentHapiness;
        GetComponent<Slider>().value = currentHapiness;
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
                Debug.Log("destroy happiness slider");
            }
        }

    }
}
