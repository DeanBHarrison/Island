using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerSpawner : MonoBehaviour
{

    public GameObject musicPlayer;
    public GameObject Menu;
    public GameObject GameManager;
    public GameObject PetController;

    private void Start()
    {
        spawnMusicPlayer();
        spawnMenu();
    }

    //instantiates a new copy of the musicplayer gameobject if it cant find the instance of musicplayer script attached to it
    public void spawnMusicPlayer()
    {
        if (MusicPlayer.instance == null)
        {
            Instantiate(musicPlayer);
        }
    }

    //instantiates a new copy of the menu gameobject if it cant find the instance of game menu script attached to it
    public void spawnMenu()
    {
        if (GameMenu.instance == null)
        {
            Instantiate(Menu);
            Debug.Log("couldn't find game menu instance, so instantiated one");
        }
    }


}
