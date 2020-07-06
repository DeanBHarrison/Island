using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showNCStatsButton : MonoBehaviour
{

    public GameObject Sliders;
    public void showNCStats()
    {

        if (!Sliders.activeInHierarchy)
        {
            Sliders.SetActive(true);
        }else
        {
            Sliders.SetActive(false);
        }
    }
}
