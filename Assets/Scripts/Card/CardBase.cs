using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(CardClick))]

public abstract class CardBase : BasePiece
{
    [NonSerialized] public BasePiece mKing;
    [NonSerialized] public CardPowerManager cardPowerManager;
    [NonSerialized] public List<Cell> Enemies = new();
    [NonSerialized] protected List<Cell> HighlightedCells = new();
    [NonSerialized] public CardMoveImage mCardMoveImage;
    public CardSO mCardSO;
    public abstract CardType _CardType { get; }

    bool usedCard;

    private void Start()
    {
        GetComponent<Image>().sprite = mCardSO._CardImage;
        usedCard = false;
        cardPowerManager = GameManager.Instance.mCardPowerManager;
        cardPowerManager.SetupThisCard(this);
    }

    public virtual void CardSetup(BasePiece basePiece, CardPowerManager _cardPowerManager)
    {
        mKing = basePiece;
        cardPowerManager = _cardPowerManager;
        mMovement = new Vector3Int(0, 15, 0);
        mColor = Color.white;
    }

    public virtual void SelectedCard()
    {
        if (usedCard) return;
        mCurrentCell = mKing.mCurrentCell;
        CheckPathing();
        ShowCells();
    }

    public virtual void UseForAllCards()
    {
        UseCard();
        mCardMoveImage.PlayPopFadeAnimation();
        gameObject.SetActive(false);

    }
    public virtual void UseCard()
    {
        if (usedCard) return;

        //usedCard = true;
        foreach (Cell c in Enemies)
            c.RemovePiece();
        ExitCard();
    }

    public virtual void ExitCard()
    {
        ClearCells();
        Enemies.Clear();
    }

    public void ClickGiveManagerSelectedCard()
    {
        if (usedCard) return;

        cardPowerManager.GetICard(this);
    }

    public GameObject GetGameObject() => gameObject;

    public abstract override void CheckPathing();
  
}
