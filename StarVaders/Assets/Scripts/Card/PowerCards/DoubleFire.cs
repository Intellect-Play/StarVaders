using System.Collections.Generic;
using Assets.Scripts.Card;
using UnityEngine;
using UnityEngine.EventSystems;

public class DoubleFire : CardBase
{
    public override CardType _CardType => CardType.DoubleFire;

 
    public override void CheckPathing()
    {
        CreateCellPathD(0, 1, mMovement.y);
    }

    private void CreateCellPathD(int xDir, int yDir, int movement)
    {
        int curX = mCurrentCell.mBoardPosition.x;
        int curY = mCurrentCell.mBoardPosition.y;

        CheckPaths(xDir, yDir, movement, curX + 1, curY);
        CheckPaths(xDir, yDir, movement, curX - 1, curY);
    }

    private void CheckPaths(int xDir, int yDir, int movement, int x, int y)
    {
        for (int i = 1; i <= movement; i++)
        {
            x += xDir;
            y += yDir;

            var cellState = mCurrentCell.mBoard.ValidateCellforCards(x, y, this);

            if (cellState == CellState.Enemy)
            {
                var cell = mCurrentCell.mBoard.mAllCells[x, y];
                Enemies.Add(cell);
                HighlightedCells.Add(cell);
                continue;
            }

            if (cellState != CellState.Free) continue;

            HighlightedCells.Add(mCurrentCell.mBoard.mAllCells[x, y]);
        }
    }

    public override void ShowCells()
    {
        foreach (var cell in HighlightedCells)
            cell.mOutlineImage.enabled = true;
    }

    public override void ClearCells()
    {
        foreach (var cell in HighlightedCells)
            cell.mOutlineImage.enabled = false;
        HighlightedCells.Clear();
    }
}
