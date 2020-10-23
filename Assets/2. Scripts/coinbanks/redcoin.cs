using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class redcoin : coin
{
    public float coinSpeed = 100;
    private void Start()
    {
        Init(transform, GameMenu.instance.StrengthMenu.transform, coinSpeed);
    }
    void Update()
    {
        MoveTowards();
    }
}
