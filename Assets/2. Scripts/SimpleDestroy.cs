using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDestroy : MonoBehaviour
{
    public GameObject prefab;
    GameObject temp;
    //
    public void DestroyOnClick()
    {
        if (temp != null) { Destroy(temp); }
        else
        { temp = Instantiate(prefab); temp.gameObject.transform.parent = GameMenu.instance.questLog.transform; }
    }
}
