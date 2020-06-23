using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pet", menuName = "Pet")]
public class Pets : ScriptableObject
{

    public new string name;
    public string description;

    public Sprite artwork;

}
