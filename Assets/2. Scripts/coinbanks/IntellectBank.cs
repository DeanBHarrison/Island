using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntellectBank : Balances
{
    static IntellectBank instance;
    public static IntellectBank Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Init();
    }

}
