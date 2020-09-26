using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoals 
{
    public QuestGoalType questGoalType;

    public int requiredAmount;
    public int currentAmount;

    public bool goalIsReached()
    {
        return (currentAmount >= requiredAmount);
    }

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
    }
}
