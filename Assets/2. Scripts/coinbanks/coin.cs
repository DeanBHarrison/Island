using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class coin : MonoBehaviour
{
    Transform ThisTransform;
    Transform TargetTransform;
    float coinspeed = 0f;
    public float coinValue = .1f;
    public Coins coins;

    public void Init(Transform A, Transform B, float speed)
    {
        ThisTransform = A;
        TargetTransform = B;
        coinspeed = speed;
    }
   
    void AddValueToBank()
    {
        switch (coins)
        {
            case Coins.RedCoin:
                StrengthBank.Instance.AddBalance(coinValue);
                PetStats.instance.strength += coinValue;
                break;
            case Coins.BlueCoin:
                IntellectBank.Instance.AddBalance(coinValue);
                PetStats.instance.intellect += coinValue;
                break;
            case Coins.YellowCoin:
                CharmBank.Instance.AddBalance(coinValue);
                PetStats.instance.charm += coinValue;
                break;
            case Coins.GreenCoin:
                SpeedBank.Instance.AddBalance(coinValue);
                PetStats.instance.speed += coinValue;
                break;
            case Coins.GoldCoin:InventoryManager.instance.GainGold(Mathf.FloorToInt(coinValue));
                break;
            default:
                break;
        }
        PetStats.instance.SaveStats();
    }
    private void OnDestroy()
    {

        
        AddValueToBank();
        MusicPlayer.instance.PlaySFX(3);

    }

    public void MoveTowards()
    {
        var step = coinspeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(ThisTransform.position, TargetTransform.position, step);
        if (Vector3.Distance(ThisTransform.position, TargetTransform.position) < .1f)
        {
            Destroy(gameObject);
        }
    }


}


