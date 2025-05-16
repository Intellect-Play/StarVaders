using System.Collections.Generic;
using UnityEngine;

public class PushBack : CardBase
{
    public override CardType _CardType => CardType.PushBack;

  

    public override void CardSetup(BasePiece king, CardPowerManager manager)
    {
        mKing = king;
        cardPowerManager = manager;
        mMovement = new Vector3Int(0, 15, 0);
        mColor = Color.white;
    }

    public override void SelectedCard()
    {
        mCurrentCell = mKing.mCurrentCell;
        ShowCells(); // optional, depending on visual feedback
    }

    public override void UseCard()
    {
        EnemySpawner.Instance.EnemyBackMoveF();
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
