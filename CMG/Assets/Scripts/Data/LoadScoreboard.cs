using System.Collections.Generic;
using UnityEngine;

public class LoadScoreboard : MonoBehaviour
{
    public ScoreboardDataList LoadScoreboardData()
    {
        if (PlayerPrefs.HasKey(SaveScoreboard.SAVE_SCOREBOARD_DATA_KEY))
        {
            string json = PlayerPrefs.GetString(SaveScoreboard.SAVE_SCOREBOARD_DATA_KEY);

            ScoreboardDataList data = JsonUtility.FromJson<ScoreboardDataList>(json);
            return data;
        }
        else
            return new ScoreboardDataList();
    }
}