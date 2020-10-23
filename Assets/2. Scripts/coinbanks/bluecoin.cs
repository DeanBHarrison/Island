using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class bluecoin : coin
{
    public float coinSpeed = 100;
    

    private void Awake()
    {
        Init(this.transform, GameMenu.instance.IntellectMenu.transform, coinSpeed);
    }
    void Update()
    {
        MoveTowards();
    }
}
