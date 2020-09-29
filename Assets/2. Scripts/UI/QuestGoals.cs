using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoals 
{
    public QuestGoalType questGoalType;

    public Sprite objectiveSprite;
    public int requiredAmount;
    public int currentAmount;
    public string PickupRequired;
    public bool goalIsReached;

    public void EnemyKilled()
    {
        if (questGoalType == QuestGoalType.Kill)
        {
            currentAmount++;
        }
    }

    public void ItemGathered()
    {
        if (questGoalType == QuestGoalType.Gather)
        {
            currentAmount++;
        }

        if(currentAmount >= requiredAmount)
        {
            goalIsReached = true;
        }
    }
}
