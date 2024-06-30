using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _newGame;

    public static GameManager Instance { get; private set;}

    private SaveGame _saveGame;
    private LoadGame _loadGame;

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

        SceneManager.sceneLoaded += LoadGame;
    }

    private void OnDestroy() 
    {
        SceneManager.sceneLoaded -= LoadGame;
    }

    public void SetNewGame()
    {
        _newGame = true;
    }

    public void LoadGame(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            if(!_newGame)
            {
                SaveGameData gameData = _loadGame.LoadGameData();
                Debug.Log("load game data " + gameData);

                CardBoardController.Instance.LoadGame(gameData);
            }
            else
            {
                CardBoardController.Instance.NewGame();
            }

            _newGame = false;
        }
    }

    public void SaveGame(SaveGameData gameData)
    {
        _saveGame.SaveGameData(gameData);
    }
}
