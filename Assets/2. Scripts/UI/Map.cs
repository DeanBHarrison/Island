using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Map : MonoBehaviour
{
    public static Map instance;
    public string ActiveSceneString;
    public TextMeshProUGUI currentLocation;

    private void Awake()
    {
        SetUpSingleton();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        SetLocationTxt();
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

    // Update is called once per frame
    void Update()
    {
    }

    public void SetLocationTxt()
    {
        ActiveSceneString = selector.ToString();
        currentLocation.SetText(ActiveSceneString);
    }

    public void HomeIconButton()
    {

        float timeToPass = 0;
        switch (selector)
        {
            case ScenesSelect.Town:
                timeToPass = 0f;
                break;
            case ScenesSelect.Woods:
                timeToPass = 0.5f;
                break;
            case ScenesSelect.Cave:
                timeToPass = 1f;
                break;
            default:
                Debug.Log("switch defaulted");
                break;
        }

        SceneManager.LoadScene("A5. Town");        
        Clock.instance.PassTime(timeToPass);
        //timepassonce was made because the clock doesnt update when fast travelling on the map due to should time pass being false when the menu is open
        Clock.instance.TimePassOnce();
    }

    public void WoodsIconButton()
    {
        float timeToPass = 0;

        switch (selector)
        {
            case ScenesSelect.Town:
                timeToPass = 0.5f;
                break;
            case ScenesSelect.Woods:
                timeToPass = 0f;
                break;
            case ScenesSelect.Cave:
                timeToPass = 0.5f;
                break;
            default:
                Debug.Log("switch defaulted");
                break;
        }
        SceneManager.LoadScene("A4. Scene1");
        Clock.instance.PassTime(timeToPass);
        //timepassonce was made because the clock doesnt update when fast travelling on the map due to should time pass being false when the menu is open
        Clock.instance.TimePassOnce();
    }

    public void CaveIconButton()
    {
        float timeToPass = 0;

        switch (selector)
        {
            case ScenesSelect.Town:
                timeToPass = 1f;
                break;
            case ScenesSelect.Woods:
                timeToPass = 0.5f;
                break;
            case ScenesSelect.Cave:
                timeToPass = 0f;
                break;
            default: Debug.Log("switch defaulted");
                break;
        }

        SceneManager.LoadScene("A4. Scene2");     
        Clock.instance.PassTime(timeToPass);
        Clock.instance.TimePassOnce();
        //timepassonce was made because the clock doesnt update when fast travelling on the map due to should time pass being false when the menu is open

    }

   
    public ScenesSelect selector;
 
}

