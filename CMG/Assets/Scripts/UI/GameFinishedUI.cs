using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinishedUI : MonoBehaviour
{
    public const string MAIN_MENU_SCENE_KEY = "MainMenu";

    [SerializeField]
    private TMP_Text _scoreTMP;
    [SerializeField]
    private TMP_Text _turnsTMP;


    public void UISetup(int score, int turns)
    {
        _scoreTMP.text = score.ToString();
        _turnsTMP.text = turns.ToString();
    }

    public void RetrunToMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE_KEY);
    }

    public void Restart()
    {
        GameManager.Instance.SetNewGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
