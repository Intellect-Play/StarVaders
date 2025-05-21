using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StrongStrike : CardBase
{

    public override CardType _CardType => CardType.StrongStrike;

   
    Cell cell;


  

    public override void CreateCellPath(int xDirection, int yDirection, int movement)
    {
        int currentX = mCurrentCell.mBoardPosition.x;
        int currentY = mCurrentCell.mBoardPosition.y;

        //CheckPaths(xDirection, yDirection, movement,  currentX,  currentY);
        CheckPaths(xDirection, yDirection, movement,  currentX,  currentY);
        //CheckPaths(xDirection, yDirection, movement,  currentX-1,  currentY);

    }

    private void CheckPaths(int xDirection, int yDirection, int movement,  int currentX,  int currentY)
    {
        for (int i = 1; i <= movement; i++)
        {
            currentX += xDirection;
            currentY += yDirection;

            CellState cellState = CellState.None;
            cellState =mCurrentCell.mBoard.ValidateCellforCards(currentX, currentY,this);

            if (cellState == CellState.Enemy)
            {

                cell = mCurrentCell.mBoard.mAllCells[currentX, currentY];
                Enemies.Add(cell);
                mEnemylightedCells.Add(cell);
                continue;
            }

            if (cellState != CellState.Free)
                continue;

            mHighlightedCells.Add(mCurrentCell.mBoard.mAllCells[currentX, currentY]);
        }
    }
        public override void CheckPathing()
    {
        CreateCellPath(0, 1, mMovement.y);
    }



}
