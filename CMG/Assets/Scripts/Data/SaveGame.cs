using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public const string SaveGameKey = "GameData";

    public void SaveGameData(SaveGameData data)
    {
        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(SaveGameKey, json);
        PlayerPrefs.Save();
    }
}

[System.Serializable]
public class SaveGameData
{
    public List<SaveCardData> Cards = new List<SaveCardData>();
    public int Turns;
    public int Matches;
}

[System.Serializable]
public class SaveCardData
{
    public int CardId;
    public Vector3 CardPosition;

    public SaveCardData(int id, Vector3 position)
    {
        CardId = id;
        CardPosition = position;
    }
}