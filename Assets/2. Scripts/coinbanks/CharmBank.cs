using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharmBank : Balances
{
    static CharmBank instance;
    public static CharmBank Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Init();
    }
    private void Update()
    {
       
    }
    public void ButtonTest()
    {
        AddBalance(1);
    }
}
