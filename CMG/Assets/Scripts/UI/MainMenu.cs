using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public const string GAME_SCENE_KEY = "GameScene";
    [SerializeField]
    private Button _loadGame;

    private void Start() 
    {
        if(PlayerPrefs.HasKey(SaveGame.SAVE_GAME_DATA_KEY))
            _loadGame.interactable = true;
        else
            _loadGame.interactable = false;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(GAME_SCENE_KEY);
    }

    public void OpenGameScene()
    {
        GameManager.Instance.SetNewGame();
        SceneManager.LoadScene(GAME_SCENE_KEY);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
