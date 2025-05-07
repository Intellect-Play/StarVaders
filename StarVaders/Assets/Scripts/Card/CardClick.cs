using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Card;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    ICard mCard;
    void OnEnable()
    {
        mCard = GetComponent<ICard>();
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
