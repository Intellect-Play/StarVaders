using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CardClick))]

public abstract class CardBase : BasePiece
{
    public BasePiece mKing;
    public CardPowerManager cardPowerManager;
    public List<Cell> Enemies = new();
    protected List<Cell> HighlightedCells = new();
    public CardMoveImage mCardMoveImage;

    public abstract CardType _CardType { get; }

    bool usedCard;

    private void Start()
    {
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
