using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Balances : MonoBehaviour
{
    public int min = 0;
    public int max = 1;
    public Slider guiSlider;
    public float currentValue;
    public float fillSpeed;
    public float totalCoins;
    public TextMeshProUGUI balanceText;
    public void Init()
    {
        guiSlider.minValue = min;
        guiSlider.maxValue = max;
        guiSlider.value = 0;
    }
    public void AddBalance(float value)
    {
        totalCoins += value;
        UpdateSliderBalance();
        balanceText.SetText($"{Mathf.Floor(totalCoins):f0}");
    }

    public void UpdateSliderBalance()
    {
        guiSlider.value = totalCoins % 1;
    }

}
