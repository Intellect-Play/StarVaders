using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : CardBase
{
    public override CardType _CardType => CardType.Move;

    public override void CheckPathing()
    {
        throw new System.NotImplementedException();
    }
    public override void SelectedCard()
    {
        mKing.CheckPathing();
        if (mKing.mHighlightedCells.Count <= 0) return;
        CardManagerMove.MoveCard = true;
        mKing.ShowCells();
        mKing.moveCard = true;
    }
    public override void UseForAllCards()
    {
        CardManagerMove.Instance.spawnedCards.Remove(this.gameObject);

        mCardMoveImage.PlayPopFadeAnimation();

    }
    public override void ExitCard()
    {
        // UseCard();
        CardManagerMove.MoveCard = false;

        base.ExitCard();
        mKing.moveCard = false;
        mKing.ClearCells();
    }
}
