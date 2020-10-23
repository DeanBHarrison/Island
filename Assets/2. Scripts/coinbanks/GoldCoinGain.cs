using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GoldCoinGain : coin
{
    public float coinSpeed = 100;
    private void Start()
    {
        Init(transform, GameMenu.instance.goldText.transform, coinSpeed);
    }
    void Update()
    {
        MoveTowards();
    }
}
