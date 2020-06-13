using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
   // Instances of other classes
    public State _startingstate;
    public LoadMenu loadMenu;

    [Header("Objects to load in")]
    public Text _currentQuestion;
    public int currentQuestionIndex = 1;
    public GameObject[] buttons;
    public int PetSpawnIndex;


    // Start is called before the first frame update
    void Start()
    {
       _currentQuestion.text = _startingstate._description;
    }

    // Update is called once per frame
    void Update()
    {
        StartTheGame();
    }

    public void StartTheGame()
    {
        if(currentQuestionIndex == 4)
        {

            Destroy(buttons[0]);
            Destroy(buttons[1]);
            loadMenu.LoadnextlevelIndex();
            PetSpawnIndex = _startingstate.petToSpawn;
        }
    }


    public void Button1()
    {
        _startingstate = _startingstate._Optionchosen[0];
        _currentQuestion.text = _startingstate._description;
        currentQuestionIndex++;
    }

    public void Button2()
    {
        _startingstate = _startingstate._Optionchosen[1];
        _currentQuestion.text = _startingstate._description;
        currentQuestionIndex++;
    }

}
