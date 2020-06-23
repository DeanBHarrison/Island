using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public static GameMenu instance;
    public GameObject gameMenu;
    public GameObject sleepButton;


    private void Awake()
    {
        instance = this;
        SetUpSingleton();
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {

            if(gameMenu.activeInHierarchy)
            {
                gameMenu.SetActive(false);
            }else
            {
                gameMenu.SetActive(true);
            }
        }
    }

    private void SetUpSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void goToSleep()
    {
        Clock.instance.PassTime(PetStats.instance.sleepLength);
        PetStats.instance.currentSleepiness = 0;
        sleepButton.SetActive(false);
        PlayerController.instance.canMove = true;
    }
}
