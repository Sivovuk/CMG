using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public const string GameSceneKey = "GameScene";
    [SerializeField]
    private Button _loadGame;

    private void Start() 
    {
        if(PlayerPrefs.HasKey(SaveGame.SaveGameKey))
            _loadGame.interactable = true;
        else
            _loadGame.interactable = false;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(GameSceneKey);
    }

    public void OpenGameScene()
    {
        GameManager.Instance.SetNewGame();
        SceneManager.LoadScene(GameSceneKey);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
