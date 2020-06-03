using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New state", menuName = "State")]
public class State : ScriptableObject
{
    public string  _description;
   

    public State[] _Optionchosen;

}
