using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SicknessSlider : MonoBehaviour
{
    public float maxSickness;
    public float currentSickness;
    public static SicknessSlider instance;
    private void Awake()
    {
        SetUpSingleton();
    }

    private void Start()
    {
        maxSickness = PetStats.instance.maxSickness;
    }
    // Update is called once per frame


    public void updateSicknessSlider()
    {
        currentSickness = PetStats.instance.currentSickness;
        GetComponent<Slider>().value = currentSickness;
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
