using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

[System.Serializable]
public class Dialogue  
{
    //npc Name
    public string name;

    // adds a larger text area to write in unity inspector
    [TextArea(3, 10)]
    //NPC Dialogue
    public string[] sentences;

    
   

}
