using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnipMan : NPC
{

    public static TurnipMan instance;

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
        if (QuestToGive)
        {
            QuestIcon.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
