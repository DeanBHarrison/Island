using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public ScenesSelect x = ScenesSelect.Town;
    private void Start()
    { 
        Map.instance.selector = x;
        Map.instance.SetLocationTxt();
    }


}


