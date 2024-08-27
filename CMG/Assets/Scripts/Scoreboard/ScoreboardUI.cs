using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreboardUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _scoreboardPrefab;
    [SerializeField]
    private Transform _scoreboardParent;

    public static ScoreboardUI Instance { get; private set; }   

    private void Awake() 
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    public void LoadScoreboardUI(ScoreboardDataList list)
    {
        if (list.ScoreboardData.Count <= 0) return;

        foreach (ScoreboardData scoreElement in list.ScoreboardData)
        {
            GameObject spawn = Instantiate(_scoreboardPrefab, _scoreboardParent);
            spawn.GetComponent<ScoreboardUIElement>().SetupElement(scoreElement.Number, scoreElement.Score, scoreElement.DateTime);
        }
    }
}
