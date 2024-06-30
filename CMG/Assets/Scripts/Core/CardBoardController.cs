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
            _cardSelected[0].Collected();
            _cardSelected[1].Collected();
            _cardSelected.Remove(_cardSelected[1]);
            _cardSelected.Remove(_cardSelected[0]);
            _matches++;

            if ((_matches*2) >= _cardGrid.NumberOfCards)
            {
                GameFinished();
                GameManager.Instance.AddScore(_matches);
            }
        }
        else
        {
            _cardSelected[0].Unflip();
            _cardSelected[1].Unflip();
            _cardSelected.Remove(_cardSelected[1]);
            _cardSelected.Remove(_cardSelected[0]);
        }

        OnValueChange?.Invoke();

        SaveGame();
    }

    private void GameFinished()
    {
        Debug.Log((_matches*2) +" >= " +  _cardGrid.NumberOfCards);
        _gameFinishedUI.gameObject.SetActive(true);
        _gameFinishedUI.UISetup(_matches, _turnsCounter);
    }

    private void TextUpdate()
    {
        _scoreTMP.text = _matches.ToString();
        _turnsTMP.text = _turnsCounter.ToString();
    }
        
    #endregion

    #region Game Load
    
    public void NewGame()
    {
        _cardGrid.StartNewGame();
    }

    public void LoadGame(GameData gameData)
    {
        Debug.Log("load game data CardBoardController " + gameData);
        _cardGrid.LoadGame(gameData);

        _turnsCounter = gameData.Turns;
        _turnsTMP.text = gameData.Turns.ToString();
        _matches = gameData.Matches;
        _scoreTMP.text = gameData.Matches.ToString();
    }

    public void SaveGame()
    {
        GameData gameData = new GameData();
        gameData.Turns = _turnsCounter;
        gameData.Matches = _matches;

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
