
using Assets.Scripts.Card;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class CardClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    CardBase mCard;
    [SerializeField] private TextMeshProUGUI mCardNameText;
    void OnEnable()
    {
        mCard = GetComponent<CardBase>();
       // mCardNameText.text = mCard._CardType.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mCard.ClickGiveManagerSelectedCard();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mCard.UseCard();
        mCard.ExitCard();
    }
}
