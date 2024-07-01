using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveScoreboard : MonoBehaviour
{
    public const string SAVE_SCOREBOARD_DATA_KEY = "ScoreboardData";

    public void SaveScoreboardData(ScoreboardDataList data)
    {
        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(SAVE_SCOREBOARD_DATA_KEY, json);
        PlayerPrefs.Save();
    }
}

[System.Serializable]
public class ScoreboardDataList
{
    public List<ScoreboardData> ScoreboardData = new List<ScoreboardData>();
}

[System.Serializable]
public class ScoreboardData
{
    public int Number;
    public int Score;
    public string DateTime;

    public ScoreboardData(int number, int score, string date)
    {
        Number = number;
        Score = score;
        DateTime = date;
    }
}