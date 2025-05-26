using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBack : CardBase
{
    public override CardType _CardType => CardType.PushBack;

  

    public override void CardSetup(BasePiece king, CardPowerManager manager)
    {
        mKing = king;
        cardPowerManager = manager;
        mMovement = mCardSO._CardPoweraArea;  //0.15.0
        mColor = Color.white;
    }

    public override void SelectedCard()
    {
        mCurrentCell = mKing.mCurrentCell;
        ShowCells(); // optional, depending on visual feedback
    }
    public override void UseForAllCards()
    {
        CardManagerMove.Instance.spawnedCards.Remove(this.gameObject);
        CameraShake.Instance.ShakeCardAttack();

        UseCard();
        mCardMoveImage.PlayPopFadeAnimation();
        GameManager.Instance.EndTurnButton(false);

        gameObject.SetActive(false);
    }
    public override void UseCard()
    {

        //StartCoroutine(PushBackTime());
        CardEffects.Instance.TornadoEffect();

    }
 
    public override void ExitCard()
    {
        ClearCells();
    }

    public override void CheckPathing()
    {
        // This card doesn’t need pathing logic (AOE / global effect)
    }

    public override void ShowCells()
    {
        // optional visual effect if needed
    }

    public override void ClearCells()
    {
        HighlightedCells.Clear();
    }
}
