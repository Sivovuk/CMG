using System.Collections.Generic;
using UnityEngine;

public class LoadScoreboard : MonoBehaviour
{
    public ScoreboardDataList LoadGameData()
    {
        if (PlayerPrefs.HasKey(SaveScoreboard.SaveScoreboardKey))
        {
            string json = PlayerPrefs.GetString(SaveScoreboard.SaveScoreboardKey);

            ScoreboardDataList data = JsonUtility.FromJson<ScoreboardDataList>(json);
            return data;
        }
        else
        {
            return new ScoreboardDataList();
        }
    }
}