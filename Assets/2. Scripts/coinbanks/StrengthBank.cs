using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthBank : Balances
{
    static StrengthBank instance;
    public static StrengthBank Instance { get => instance; set => instance = value; }

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
}
