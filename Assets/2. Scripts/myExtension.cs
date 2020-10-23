using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyExtension
{ 
    public static string ColorString(this string text, Color color) 
    { return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>"; } 
}
