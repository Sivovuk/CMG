using System;
using TMPro;
using UnityEngine;

public class ScoreboardUIElement : MonoBehaviour 
{
    [SerializeField]
    private TMP_Text _numberTMP;
    [SerializeField]
    private TMP_Text _scoreTMP;
    [SerializeField]
    private TMP_Text _dateTMP;

    public void SetupElement(int number, int score, string date)
    {
        _numberTMP.text = number.ToString();
        _scoreTMP.text = score.ToString();
        _dateTMP.text = date;
    }
}