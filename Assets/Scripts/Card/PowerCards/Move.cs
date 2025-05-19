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
    {Debug.Log("SelectedCard");
        mKing.CheckPathing();
        CardManagerMove.MoveCard = true;
        mKing.ShowCells();
        mKing.moveCard = true;
    }
    public override void UseForAllCards()
    {
        Debug.Log("UseForAllCards");
        mCardMoveImage.PlayPopFadeAnimation();
    }
    public override void ExitCard()
    {
        // UseCard();
        CardManagerMove.MoveCard = false;

        Debug.Log("ExitCard");
        base.ExitCard();
        mKing.moveCard = false;
        mKing.ClearCells();
    }
}
