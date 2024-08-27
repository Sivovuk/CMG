using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardBoardController : MonoBehaviour
{
    [Header("Cards Setup")]
    [SerializeField]
    private List<CardController> _cardSelected = new List<CardController>();
    [SerializeField]    
    private CardDynamicDisplayGrid _cardGrid;
    [SerializeField]
    private GameFinishedUI _gameFinishedUI;
    [SerializeField]
    private TMP_Text _scoreTMP;
    [SerializeField]
    private TMP_Text _turnsTMP;

    public static CardBoardController Instance {get; private set;}

    private UnityAction OnValueChange;

    private int _matches = 0;
    private int _turnsCounter = 0;
    private int _comboCounter = 0;
    private int _highestCombo = 1;

    private void Awake() 
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        OnValueChange += TextUpdate;
    }

    private void OnDestroy() 
    {
        OnValueChange -= TextUpdate;
    }

    #region Game

    public void CardSelected(CardController card)
    {
        _cardSelected.Add(card);
        CardCheck();
    }

    private void CardCheck()
    {
        if (_cardSelected.Count <= 1) return;

        _turnsCounter++;

        if (_cardSelected[0].CardInfo.CardID == _cardSelected[1].CardInfo.CardID)
        {
            _cardSelected[1].Collected();
            _cardSelected[0].Collected();
            _cardSelected.Remove(_cardSelected[1]);
            _cardSelected.Remove(_cardSelected[0]);
            AudioController.Instance.PlayAudio(AudioController.Instance.Match);

            _matches++;
            _comboCounter++;

            if(_highestCombo < _comboCounter && _comboCounter >= 2)
                _highestCombo = _comboCounter;

            if ((_matches*2) >= _cardGrid.NumberOfCards)
            {
                GameFinished();
                GameManager.Instance.AddScore(_matches + _highestCombo - 1);
                return;
            }
        }
        else
        {
            _cardSelected[0].Unflip();
            _cardSelected[1].Unflip();
            _cardSelected.Remove(_cardSelected[1]);
            _cardSelected.Remove(_cardSelected[0]);

            _comboCounter = 0;
            AudioController.Instance.PlayAudio(AudioController.Instance.Mismatch);
        }

        OnValueChange?.Invoke();

        StartCoroutine(SaveGame());
    }

    private void GameFinished()
    {
        GameManager.Instance.RemoveSaveData();
        _gameFinishedUI.gameObject.SetActive(true);
        _gameFinishedUI.UISetup(_matches + _highestCombo - 1, _turnsCounter);
        AudioController.Instance.PlayAudio(AudioController.Instance.GameEnd);
    }

    private void TextUpdate()
    {
        _scoreTMP.text = (_matches + _highestCombo - 1).ToString();
        _turnsTMP.text = _turnsCounter.ToString();
    }
        
    #endregion

    #region Game Load
    
    public void NewGame(int difficultyIndex)
    {
        _cardGrid.StartNewGame(difficultyIndex);
    }

    public void LoadGame(GameData gameData)
    {
        _cardGrid.LoadGame(gameData, gameData.Rows, gameData.Columns);

        _turnsCounter = gameData.Turns;
        _turnsTMP.text = gameData.Turns.ToString();
        _matches = gameData.Matches;
        _scoreTMP.text = gameData.Matches.ToString();
        _comboCounter = gameData.ComboCounter;
        _highestCombo = gameData.HighestCombo;
    }

    public IEnumerator SaveGame()
    {
        yield return new WaitForSeconds(0.1f);
        
        GameData gameData = new GameData();
        gameData.Turns = _turnsCounter;
        gameData.Matches = _matches;
        gameData.ComboCounter = _comboCounter;
        gameData.HighestCombo = _highestCombo;
        gameData.Rows = _cardGrid.Rows;
        gameData.Columns = _cardGrid.Columns;

        for (int i = 0; i < transform.childCount; i++)
        {
            int id = transform.GetChild(i).GetComponent<CardController>().CardInfo.CardID;
            Vector3 position = transform.GetChild(i).position;
            gameData.Cards.Add(new SaveCardData(id, position));
        }
        
        GameManager.Instance.SaveGame(gameData);
    }
    
    #endregion
}
