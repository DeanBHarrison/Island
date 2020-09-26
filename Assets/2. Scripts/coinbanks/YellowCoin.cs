using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 class YellowCoin : coin
{
    public float coinSpeed = 100;
    private void Awake()
    {
        Init(this.transform, GameMenu.instance.CharmMenu.transform, coinSpeed);
    }
    void Update()
    {
        MoveTowards();
    }
}
