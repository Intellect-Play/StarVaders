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

        mKing.ShowCells();
        mKing.moveCard = true;
    }
    public override void UseForAllCards()
    {
        UseCard();
        mCardMoveImage.PlayPopFadeAnimation();
    }
    public override void ExitCard()
    {
        base.ExitCard();
        mKing.moveCard = false;
        mKing.ClearCells();
    }
}
