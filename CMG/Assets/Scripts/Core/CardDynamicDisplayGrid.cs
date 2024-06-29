using System.Collections.Generic;
using UnityEngine;

public class CardDynamicDisplayGrid : MonoBehaviour
{
    [Header("Board Setup")]
    [SerializeField]
    private int _rows = 1;
    [SerializeField]
    private int _columns = 1;
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

    void Start()
    {
        _containerRect = GetComponent<RectTransform>();
        CalculateCardSize();
        SpawnCards();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CalculateCardSize();
            SpawnCards();
        }
    }

    void CalculateCardSize()
    {
        // Calculate the size of each card based on the container size and number of cards
        float containerWidth = _containerRect.rect.width - (_columns - 1) * _horizontalSpacing;
        float containerHeight = _containerRect.rect.height - (_rows - 1) * _verticalSpacing;

        _cardWidth = containerWidth / _columns;
        _cardHeight = containerHeight / _rows;

        
    }

    void SpawnCards()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        int maxNumberOfCards = (_columns * _rows) / 2;

        List<Card> cardsShuffle = new List<Card>();

        for (int i = 0; i <= maxNumberOfCards; i++)
        {
            cardsShuffle.Add(_cardsVariations[Random.Range(0, _cardsVariations.Count)]);
            cardsShuffle.Add(_cardsVariations[Random.Range(0, _cardsVariations.Count)]);
        }

        CardShuffle.Shuffle(cardsShuffle);

        int counter = 0;

        float newSize = _cardWidth <= _cardHeight ? _cardWidth : _cardHeight;

        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _columns; col++)
            {
                GameObject card = Instantiate(cardsShuffle[counter].CardPrefab, transform);
                RectTransform cardRect = card.GetComponent<RectTransform>();

                card.GetComponent<CardController>().CardSetup(cardsShuffle[counter]);
                counter++;

                // Set card size
                cardRect.sizeDelta = new Vector2(newSize, newSize);

                // Set card position
                float xPos = col * (newSize + _horizontalSpacing) - _containerRect.rect.width / 2 + newSize / 2;
                float yPos = row * (newSize + _verticalSpacing) - _containerRect.rect.height / 2 + newSize / 2;
                cardRect.localPosition = new Vector3(xPos, yPos, 0);
            }
        }
    }
}