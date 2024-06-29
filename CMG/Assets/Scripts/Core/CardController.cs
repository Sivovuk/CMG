using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class CardController : MonoBehaviour
{
    public Card CardInfo {get; private set;}

    private Button _button;
    [SerializeField]
    private Image _cardIcon;

    private void Start() 
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(FlipIn);
    }

    private void OnDestroy() 
    {
        _button.GetComponent<Button>().onClick.RemoveListener(FlipIn);
    }

    private void Update() 
    {
        
    }

    public void CardSetup(Card cardInfo)
    {
        CardInfo = cardInfo;
    }

    public void CardClicked()
    {
        CardBoardController.Instance.CardSelected(this);
    }

    public void Collected()
    {
        Destroy(gameObject);
    }

    public void Unflip()
    {
        _cardIcon.enabled = false;
        //LeanTween.scaleX(gameObject, 0.05f, 0.05f).setOnComplete(FlipOut);
    }

    public void FlipIn()
    {
        _cardIcon.enabled = true;
        //CardClicked();
        LeanTween.scaleX(gameObject, 0.05f, 0.05f).setOnComplete(FlipOut);
    }

    public void FlipOut()
    {
        _cardIcon.enabled = true;
        LeanTween.scaleX(gameObject, 1f, 0.05f).setOnComplete(CardClicked);
    }

}
