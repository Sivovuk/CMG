using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameFinishedUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreTMP;
    [SerializeField]
    private TMP_Text _turnsTMP;


    public void UISetup(int score, int turns)
    {
        _scoreTMP.text = score.ToString();
        _turnsTMP.text = turns.ToString();
    }
}
