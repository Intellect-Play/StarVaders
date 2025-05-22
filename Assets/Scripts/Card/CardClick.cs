using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class CardClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private float triggerHeight = 150f;
    [SerializeField] private float returnDuration = 0.5f;

    private float startPosY;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CardBase card;

    public Transform spawnParent;
    public CardMoveImage moveImage;
    public CardTypeMove cardType;

    [HideInInspector] public bool Hovering;
    [HideInInspector] public bool CanDrag;

    Vector3 GetPos;
    private int originalSiblingIndex;

    public CardState _CardState { get; private set; }

    public enum CardState { Idle, IsDragging, Played }

    public void Initialize(Transform parent, Canvas canvasRef, GameObject moveImageObj)
    {
        canvas = canvasRef;
        spawnParent = parent;
        moveImage = moveImageObj.GetComponent<CardMoveImage>();
        moveImage.SetTarget(gameObject, card.mCardSO._CardImage);
        card.mCardMoveImage = moveImage;
        originalSiblingIndex = transform.GetSiblingIndex();

    }

    private void OnEnable()
    {
        CanDrag = false;
        rectTransform = GetComponent<RectTransform>();
        card = GetComponent<CardBase>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (CardManagerMove.MoveCard) return;
        GetPos = transform.position;
        CanDrag = true;
        startPosY = transform.position.y;
        //originalSiblingIndex = transform.GetSiblingIndex();

        //transform.parent = canvas.transform;
        //moveImage.transform.parent = canvas.transform;
        card.ClickGiveManagerSelectedCard();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!CanDrag) return;
        CanDrag = false;

        if (transform.position.y - startPosY > triggerHeight)
        {
            card.UseForAllCards();
            return;
        }

        ResetCardPosition();
        card.ExitCard();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!CanDrag || canvas == null) return;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!CanDrag || CardManagerMove.MoveCard) return;
        _CardState = CardState.IsDragging;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!CanDrag) return;
        _CardState = CardState.Idle;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (CardManagerMove.MoveCard) return;
        Hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!CanDrag) return;
        Hovering = false;
    }

    private void ResetCardPosition()
    {
        Debug.Log("Resetting card position");
        //transform.parent = spawnParent;
        //transform.SetSiblingIndex(originalSiblingIndex);
        //moveImage.ReturnParent();
        //transform.localPosition = Vector3.zero;
        transform.position = GetPos;
    }
}