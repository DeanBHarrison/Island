using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    //this adds an option to unity and initialises a variable of type "state" which is a class i made, this lets me
    // 
    public State _startingstate;
    public Text _currentQuestion;


    // Start is called before the first frame update
    void Start()
    {
       _currentQuestion.text = _startingstate._description;
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void Button1()
    {
        _startingstate = _startingstate._Optionchosen[0];
        _currentQuestion.text = _startingstate._description;
    }

    ass

    public void Button2()
    {
        _startingstate = _startingstate._Optionchosen[1];
        _currentQuestion.text = _startingstate._description;
    }

}
