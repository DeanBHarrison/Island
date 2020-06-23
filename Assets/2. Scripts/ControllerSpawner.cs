using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSpawner : MonoBehaviour
{

    public GameObject musicPlayer;
    public GameObject Menu;

    private void Start()
    {
        if(!musicPlayer.activeInHierarchy)
        {
            Instantiate(musicPlayer);
        }

        if (!musicPlayer.activeInHierarchy)
        {
            Instantiate(musicPlayer);
        }
    }

}
