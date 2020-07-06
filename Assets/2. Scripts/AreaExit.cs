using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{

    public LoadMenu LoadMenu;
    AreaEntrance theEntrance;
    public string areaTransitionName;

    public string sceneToLoad;
    public string currentArea;

   
    void Start()
    {
        SetAreaTransitionName();
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerController.instance.lastArea = areaTransitionName;
            LoadMenu.LoadnextlevelString(sceneToLoad);
        }
    }

    public void SetAreaTransitionName()
    {
        theEntrance = GetComponentInChildren<AreaEntrance>();
        theEntrance.transitionName = areaTransitionName;
    }
}
