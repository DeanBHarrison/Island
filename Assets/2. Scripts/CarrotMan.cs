using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotMan : NPC
{
    public static CarrotMan instance;

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
                Debug.Log("Turnipman instance deleted");
                Destroy(gameObject);
            }
        }
    }
    // Start is called before the first frame update
    public void Start()
    {
        LoadData();
        QuestAvailable();
        InvokeRepeating("Chatter", 4f, 20f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
