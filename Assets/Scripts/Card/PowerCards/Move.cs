using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : CardBase
{
    public override CardType _CardType => CardType.Move;
    public void Awake()
    {
        mCardPower = SaveManager.Instance.cardDataList.cards.Find(x => x.name == _CardType.ToString()).power;

    }
    public override void CheckPathing()
    {
        throw new System.NotImplementedException();
    }
    public override void SelectedCard(bool moveActive = false)
    {
        mKing.CheckPathing();
        if (mKing.mHighlightedCells.Count <= 0) return;

        
        mKing.ShowCells();
        mKing.moveCard = true;
    }
    public override void UseForAllCards()
    {
        CardManagerMove.MoveCard = true;
        CardManagerMove.Instance.RemoveSpawnCard(this.gameObject);

        mCardMoveImage.PlayPopFadeAnimation();
        gameObject.SetActive(false);
    }
    public override void UseForMoveCards()
    {
        CardManagerMove.Instance.RemoveSpawnCard(this.gameObject);

        mCardMoveImage.PlayPopFadeAnimation();
        gameObject.SetActive(false);
    }
    public void ExitFormOneTouch()
    {
        CardManagerMove.Instance.RemoveSpawnCard(this.gameObject);

        mCardMoveImage.PlayPopFadeAnimation();
        gameObject.SetActive(false);
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
