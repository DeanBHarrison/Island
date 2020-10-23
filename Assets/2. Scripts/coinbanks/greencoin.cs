using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class greencoin : coin
{
    public float coinSpeed = 100;
    private void Awake()
    {
        Init(this.transform, GameMenu.instance.SpeedMenu.transform, coinSpeed);
    }
    void Update()
    {
        MoveTowards();
    }
}
