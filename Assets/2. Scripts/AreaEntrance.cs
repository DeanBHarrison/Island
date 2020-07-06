using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    public string transitionName = "placeholder";


    // Start is called before the first frame update
    void Start()
    {
        if (PlayerController.instance)
        {
            if (transitionName == PlayerController.instance.lastArea)
            {
                PlayerController.instance.transform.position = transform.position;
            }
        }

        //UIFade.instance.FadeFromBlack();
        //GameManager.instance.fadingBetweenareas = false;
    }

    // Update is called once per frame

}
