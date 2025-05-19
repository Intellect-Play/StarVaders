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

    private bool used;

    private void Start()
    {
        GetComponent<Image>().sprite = mCardSO._CardImage;
        used = false;
        cardPowerManager = GameManager.Instance.mCardPowerManager;
        cardPowerManager.SetupThisCard(this);
    }

    public virtual void CardSetup(BasePiece king, CardPowerManager manager)
    {
        mKing = king;
        cardPowerManager = manager;
        mMovement = new Vector3Int(0, 15, 0);
        mColor = Color.white;
    }

    public virtual void SelectedCard()
    {
        if (used) return;
        mCurrentCell = mKing.mCurrentCell;
        CheckPathing();
        ShowCells();
    }

    public virtual void UseForAllCards()
    {
        CameraShake.Instance.Shake(0.5f, 0.3f, 10, 90f);

        UseCard();
        mCardMoveImage.PlayPopFadeAnimation();
        gameObject.SetActive(false);
    }

    public virtual void UseCard()
    {
        if (used) return;
        foreach (var cell in Enemies)
            cell.RemovePiece();
        ExitCard();
    }

    public virtual void ExitCard()
    {
        ClearCells();
        Enemies.Clear();
    }

    public void ClickGiveManagerSelectedCard()
    {
        if (used) return;
        cardPowerManager.GetICard(this);
    }

    public GameObject GetGameObject() => gameObject;

    public abstract override void CheckPathing();
    public abstract CardType _CardType { get; }
}

