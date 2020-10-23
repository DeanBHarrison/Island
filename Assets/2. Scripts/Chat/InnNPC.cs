using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnNPC : NPC
{
    public static InnNPC instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }


    private void Start()
    {
            InvokeRepeating("Chatter", 1f, 15f);
    }


}
