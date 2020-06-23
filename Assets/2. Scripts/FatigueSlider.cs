using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FatigueSlider : MonoBehaviour
{
    public int maxFatigue;
    public float currentFatigue;


    private void Start()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        maxFatigue = PetStats.instance.maxFatigue;
        currentFatigue = PetStats.instance.currentFatigue;
        GetComponent<Slider>().value = currentFatigue;
    }
}
