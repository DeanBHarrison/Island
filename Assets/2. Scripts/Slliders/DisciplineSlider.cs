using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisciplineSlider : MonoBehaviour
{
    public float maxDiscipline;
    public float currentDiscipline;
    public static DisciplineSlider instance;
    private void Awake()
    {
        SetUpSingleton();
    }

    private void Start()
    {
        maxDiscipline = PetStats.instance.maxDiscipline;
    }
    // Update is called once per frame


    public void updateDisciplineSlider()
    {
        currentDiscipline = PetStats.instance.currentDiscipline;
        GetComponent<Slider>().value = currentDiscipline;
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
