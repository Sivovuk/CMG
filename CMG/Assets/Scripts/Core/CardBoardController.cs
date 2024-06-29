using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBoardController : MonoBehaviour
{
    [Header("Cards Setup")]
    [SerializeField]
    private List<CardController> _cardSelected = new List<CardController>();

    public static CardBoardController Instance {get; private set;}

    private void Awake() 
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        
    }

    public void CardSelected(CardController card)
    {
        _cardSelected.Add(card);
        CardCheck();
    }

    private void CardCheck()
    {
        if (_cardSelected.Count <= 1) return;

        if (_cardSelected[0].CardInfo.CardID == _cardSelected[1].CardInfo.CardID)
        {
            _cardSelected[0].Collected();
            _cardSelected[1].Collected();
            _cardSelected.Remove(_cardSelected[1]);
            _cardSelected.Remove(_cardSelected[0]);
        }
        else
        {
            _cardSelected[0].Unflip();
            _cardSelected[1].Unflip();
            _cardSelected.Remove(_cardSelected[1]);
            _cardSelected.Remove(_cardSelected[0]);
        }
    }

}
