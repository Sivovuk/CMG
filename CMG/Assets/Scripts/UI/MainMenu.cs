using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public const string GameSceneKey = "GameScene";

    public void OpenGameScene()
    {
        SceneManager.LoadScene(GameSceneKey);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
