using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Quest  
{
    public bool isActive;

    public string title;
    public string description;
    public Sprite RewardSprite;
    public Coins coinReward;
    public string itemReward;

    public QuestGoals goal;



}
