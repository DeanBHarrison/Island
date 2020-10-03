using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class NpcDataSaveData
{
    public string NPCNameForFile = "NPC NAME HERE";

    #region World Data
    public bool QuestToGive;
    #endregion
    public NpcDataSaveData(string NPCName, bool questAccepted)
    {
        NPCNameForFile = NPCName;
        QuestToGive = questAccepted;
    }

}
[System.Serializable]
public class PlayerStats
{
    public float Strength;
    public float Speed;

    public PlayerStats(float strength, float speed)
    {
        Strength = strength;
        Speed = speed;
    }
}

public static class SaveLoadSystem
{
    public static NpcDataSaveData LoadData(string npcName)
    {
        if (File.Exists($"{Application.dataPath}/SaveData/{npcName}_data.json"))
        {
            string x = File.ReadAllText($"{Application.dataPath}/SaveData/{npcName}_data.json");
            return JsonUtility.FromJson<NpcDataSaveData>(x);
        }
        else
        {
            return new NpcDataSaveData(npcName, true);
        }
    }

    public static void SaveData(string npcName, bool questAccepted)
    {
        NpcDataSaveData saveData = new NpcDataSaveData(npcName, questAccepted);
        string jsonData = JsonUtility.ToJson(saveData, true);
        //File.WriteAllText($"{Application.dataPath}/SaveData/{npcName}_data.json", jsonData);
        Debug.Log($"File Saved : {Application.dataPath}/SaveData/{npcName}_data.json");
    }

    public static void SavePlayerData(float strength, float speed)
    {
        PlayerStats savePlayerData = new PlayerStats(strength, speed);
        string jsonData = JsonUtility.ToJson(savePlayerData, true);
       // File.WriteAllText($"{Application.dataPath}/SaveData/playerstats_data.json", jsonData);
        Debug.Log($"File Saved : {Application.dataPath}/SaveData/playerstats_data.json");
    }

    
    public static PlayerStats LoadPlayerStats()
    {
        if (File.Exists($"{Application.dataPath}/SaveData/playerstats_data.json"))
        {
            string x = File.ReadAllText($"{Application.dataPath}/SaveData/playerstats_data.json");
            return JsonUtility.FromJson<PlayerStats>(x);
        }
        else
        {
            return new PlayerStats(0, 0);
        }
    }
}
