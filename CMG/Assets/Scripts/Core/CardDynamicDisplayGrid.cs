using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardDynamicDisplayGrid : MonoBehaviour
{
    public int Rows {get; private set;}
    public int Columns {get; private set;}
    
    [Header("Board Setup")]
    [SerializeField]
    private float _horizontalSpacing = 10f;
    [SerializeField]
    private float _verticalSpacing = 10f;

    [Header("Card Setup")]
    [SerializeField]
    private List<Card> _cardsVariations = new List<Card>();

    private RectTransform _containerRect;
    private float _cardWidth;
    private float _cardHeight;

    public int NumberOfCards {get; private set;}

    void Awake()
    {
        _containerRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CalculateCardSize();
            SpawnNewCards();
        }
    }

    public void StartNewGame(int difficultyIndex)
    {
        if(difficultyIndex == 1)
        {
            Rows = 2;
            Columns = 3;
        }
        else if(difficultyIndex == 2)
        {
            Rows = 5;
            Columns = 4;
        }
        else if(difficultyIndex == 3)
        {
            Rows = 6;
            Columns = 6;
        }

        CalculateCardSize();
        SpawnNewCards();
    }

    void CalculateCardSize()
    {
        float containerWidth = _containerRect.rect.width - (Columns - 1) * _horizontalSpacing;
        float containerHeight = _containerRect.rect.height - (Rows - 1) * _verticalSpacing;

        _cardWidth = containerWidth / Columns;
        _cardHeight = containerHeight / Rows;

        
    }

    void SpawnNewCards()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        int maxNumberOfCards = (Columns * Rows) / 2;
        NumberOfCards = Columns * Rows;

        List<Card> cardsShuffle = new List<Card>();

        for (int i = 0; i < maxNumberOfCards; i++)
        {
            int randomCard = Random.Range(0, _cardsVariations.Count);
            cardsShuffle.Add(_cardsVariations[randomCard]);
            cardsShuffle.Add(_cardsVariations[randomCard]);
        }

        CardShuffle.Shuffle(cardsShuffle);

        float newSize = _cardWidth <= _cardHeight ? _cardWidth : _cardHeight;

        int counter = 0;

        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                GameObject card = Instantiate(cardsShuffle[counter].CardPrefab, transform);
                RectTransform cardRect = card.GetComponent<RectTransform>();

                card.GetComponent<CardController>().CardSetup(cardsShuffle[counter]);
                counter++;

                cardRect.sizeDelta = new Vector2(newSize, newSize);

                float xPos = col * (newSize + _horizontalSpacing) - _containerRect.rect.width / 2 + newSize / 2;
                float yPos = row * (newSize + _verticalSpacing) - _containerRect.rect.height / 2 + newSize / 2;
                cardRect.localPosition = new Vector3(xPos, yPos, 0);
            }
        }
    }

    public void LoadGame(GameData gameData, int rows, int columns)
    {
        Rows = rows;
        Columns = columns;

        CalculateCardSize();

        float newSize = _cardWidth <= _cardHeight ? _cardWidth : _cardHeight;
        NumberOfCards = gameData.Cards.Count + (gameData.Matches*2);

        foreach (SaveCardData card in gameData.Cards)
        {
            Card currentCard = _cardsVariations.FirstOrDefault(x => x.CardID == card.CardId);
            GameObject spawnCard = Instantiate(currentCard.CardPrefab, card.CardPosition, Quaternion.identity, transform);

            RectTransform cardRect = spawnCard.GetComponent<RectTransform>();
            cardRect.sizeDelta = new Vector2(newSize, newSize);

            spawnCard.GetComponent<CardController>().CardSetup(currentCard);
        }
    }
}