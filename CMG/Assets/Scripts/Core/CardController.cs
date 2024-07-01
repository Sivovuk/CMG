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
    [SerializeField]
    private float _flipAnimationSpeed = 1f;

    private bool _isClicked;

    private void Start() 
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Flip);
    }

    private void OnDestroy() 
    {
        _button.GetComponent<Button>().onClick.RemoveListener(Flip);
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
        LeanTween.scaleX(gameObject, 0.05f, _flipAnimationSpeed).setOnComplete(() => ShowCardIcon(false));
    }

    public void Flip()
    {
        if (_isClicked) return;
        _isClicked = true;

        LeanTween.scaleX(gameObject, 0.05f, _flipAnimationSpeed).setOnComplete(() => ShowCardIcon(true));
        AudioController.Instance.PlayAudio(AudioController.Instance.Flip);
    }

    public void ShowCardIcon(bool value)
    {
        _cardIcon.enabled = value;
        if(value)
            LeanTween.scaleX(gameObject, 1f, _flipAnimationSpeed).setOnComplete(CardClicked);
        else
            LeanTween.scaleX(gameObject, 1f, _flipAnimationSpeed).setOnComplete(() => _isClicked = false);
    }   

}
