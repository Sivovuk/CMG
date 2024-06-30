using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _newGame;

    [SerializeField]
    private ScoreboardDataList _scoreboardData = new ScoreboardDataList();

    public static GameManager Instance { get; private set;}

    private SaveGame _saveGame;
    private LoadGame _loadGame;
    private SaveScoreboard _saveScoreboard;
    private LoadScoreboard _loadScoreboard;

    private void Awake() 
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(this);
    }

    private void Start() 
    {
        _loadGame = GetComponent<LoadGame>();
        _saveGame = GetComponent<SaveGame>();
        _loadScoreboard = GetComponent<LoadScoreboard>();
        _saveScoreboard = GetComponent<SaveScoreboard>();

        SceneManager.sceneLoaded += LoadGame;
        SceneManager.sceneLoaded += LoadScoreboardData;

        LoadScoreboardData(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnDestroy() 
    {
        SceneManager.sceneLoaded -= LoadGame;
        SceneManager.sceneLoaded -= LoadScoreboardData;
    }

    public void SetNewGame()
    {
        _newGame = true;
    }

    #region Game Data

    public void LoadGame(Scene scene, LoadSceneMode mode)
    {
        
        if (scene.name != MainMenu.GameSceneKey) return;

        if(!_newGame)
        {
            GameData gameData = _loadGame.LoadGameData();
            Debug.Log("load game data " + gameData);

            CardBoardController.Instance.LoadGame(gameData);
        }
        else
        {
            CardBoardController.Instance.NewGame();
        }

        _newGame = false;
    
    }

    public void SaveGame(GameData gameData)
    {
        _saveGame.SaveGameData(gameData);
    }

    #endregion

    #region Scoreboard Data

    public void AddScore(int score)
    {
        DateTime currentDate = DateTime.Today;
        string formattedDate = currentDate.ToString("yyyy-MM-dd");

        if (_scoreboardData.ScoreboardData.Count <= 0)
        {
            _scoreboardData.ScoreboardData.Add(new ScoreboardData(1, score, formattedDate));
        }
        else
        {
            for (int i = 0; i < _scoreboardData.ScoreboardData.Count; i++)
            {
                if(score >= _scoreboardData.ScoreboardData[i].Score)
                {
                    
                    _scoreboardData.ScoreboardData.Insert(i, new ScoreboardData(i+1, score, formattedDate));
                    break;
                }
            }
        }

        SaveScoreboardData();
    }

    public void LoadScoreboardData(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != GameFinishedUI.MainMenuSceneKey) return;
        if (!PlayerPrefs.HasKey(SaveScoreboard.SaveScoreboardKey)) return;

        ScoreboardDataList data = _loadScoreboard.LoadGameData();
        ScoreboardUI.Instance.LoadScoreboardUI(data);
    }

    public void SaveScoreboardData()
    {
        _saveScoreboard.SaveScoreboardData(_scoreboardData);
    }

    #endregion
}
