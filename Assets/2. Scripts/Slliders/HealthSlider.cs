using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public static HealthSlider instance;
    private void Awake()
    {
        SetUpSingleton();
    }

    private void Start()
    {
        maxHealth = PetStats.instance.maxHealth;
    }
    // Update is called once per frame


    public void updateHealthSlider()
    {
        currentHealth = PetStats.instance.currentHealth;
        GetComponent<Slider>().value = currentHealth;
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
