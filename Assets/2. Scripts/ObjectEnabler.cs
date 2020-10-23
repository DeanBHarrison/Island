using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEnabler : MonoBehaviour
{

    public static ObjectEnabler instance;

    public GameObject UICanvas;
    public GameObject Pet;

    private void Awake()
    {
        instance = this;
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    public void disableUI()
    {
        UICanvas.SetActive(false);
        Pet.SetActive(false);
    }

    public void enableUI()
    {
        UICanvas.SetActive(true);
        Pet.SetActive(true);
    }
}
