using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerSlider : MonoBehaviour
{
    public float maxHunger;
    public float currentHunger;
    public static HungerSlider instance;
    private void Awake()
    {
        SetUpSingleton();
    }

    private void Start()
    {
        maxHunger = PetStats.instance.maxHunger;
    }
    // Update is called once per frame

    private void Update()
    {
       
    }


    public void updateHungerSlider()
    {
        currentHunger = PetStats.instance.currentHunger;
        GetComponent<Slider>().value = currentHunger;
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
