using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    public static GameAssets i
    {    
        get
        {
            if (_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }
     
    [Header("Assets")]
    public Transform chatBubble;

    [Header("Coins")]
    public Transform GOLDCoin;
    public Transform GOLDCoinGain;
    public Transform GreenStatCoin;
    public Transform RedStatCoin;
    public Transform YellowStatCoin;
    public Transform BlueStatCoin;
    public Transform BigGreenStatCoin;
    public Transform BigRedStatCoin;
    public Transform BigYellowStatCoin;
    public Transform BigBlueStatCoin;
    public Transform SmallGreenStatCoin;
    public Transform SmallRedStatCoin;
    public Transform SmallYellowStatCoin;
    public Transform SmallBlueStatCoin;

    [Header("DontDestroyObjects")]
    public Transform GameManager;
    public Transform MusicPlayer;
    public Transform PetController;
    public Transform UICanvas;

    [Header("UI Objects")]
    public Image questRewardImage;
    public TextMeshProUGUI questTitleText;
    public TextMeshProUGUI questDescriptionText;
    public GameObject questWindow;

    private void Update()
    {

    }
}
