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

    public int DifficultyLevel { get; private set; }

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
        SceneManager.sceneLoaded += PlayMusic;

        LoadScoreboardData();
    }

    private void OnDestroy() 
    {
        SceneManager.sceneLoaded -= LoadGame;
        SceneManager.sceneLoaded -= LoadScoreboardData;
        SceneManager.sceneLoaded -= PlayMusic;
    }

    public void SetNewGame()
    {
        _newGame = true;
    }

    #region Game Data

    public void LoadGame(Scene scene, LoadSceneMode mode)
    {
        
        if (scene.name != MainMenu.GAME_SCENE_KEY) return;

        if(!_newGame)
        {
            GameData gameData = _loadGame.LoadGameData();

            CardBoardController.Instance.LoadGame(gameData);
        }
        else
            CardBoardController.Instance.NewGame(DifficultyLevel);

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

         // Find the position to insert the new element
        int insertIndex = _scoreboardData.ScoreboardData.Count;
        for (int i = 0; i < _scoreboardData.ScoreboardData.Count; i++)
        {
            if (score > _scoreboardData.ScoreboardData[i].Score)
            {
                insertIndex = i;
                break;
            }
        }

        // Insert the element at the correct position
        _scoreboardData.ScoreboardData.Insert(insertIndex, new ScoreboardData(insertIndex+1, score, formattedDate));

        // Ensure the list does not exceed the maximum size
        if (_scoreboardData.ScoreboardData.Count > 10)
            _scoreboardData.ScoreboardData.RemoveAt(_scoreboardData.ScoreboardData.Count - 1); // Remove the smallest element (last in the list)

        for (int i = 0; i < _scoreboardData.ScoreboardData.Count; i++)
            _scoreboardData.ScoreboardData [i].Number = i+1;

        SaveScoreboardData();
    }

    public void LoadScoreboardData(Scene scene, LoadSceneMode mode)
    {
        LoadScoreboardData();
    }

    public void LoadScoreboardData()
    {
        if (SceneManager.GetActiveScene().name != GameFinishedUI.MAIN_MENU_SCENE_KEY) return;
        if (!PlayerPrefs.HasKey(SaveScoreboard.SAVE_SCOREBOARD_DATA_KEY)) return;

        ScoreboardDataList data = _loadScoreboard.LoadScoreboardData();
        ScoreboardUI.Instance.LoadScoreboardUI(data);
    }

    public void SaveScoreboardData()
    {
        _saveScoreboard.SaveScoreboardData(_scoreboardData);
    }

    public void RemoveSaveData()
    {
        _saveGame.RemoveData();
    }

    #endregion

    public void SetDifficultyLevel(int index)
    {
        DifficultyLevel = index;
    }

    private void PlayMusic(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == GameFinishedUI.MAIN_MENU_SCENE_KEY)
        {
            AudioController.Instance.PlayAudio(AudioController.Instance.BackgroundMusic);
            AudioController.Instance.StopAudio(AudioController.Instance.GameEnd);
        }
        else
            AudioController.Instance.StopAudio(AudioController.Instance.BackgroundMusic);
    }
}
