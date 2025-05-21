using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bomb3x3 : CardBase

{
    
    public override CardType _CardType => CardType.Bomb3x3;

    
    Cell cell;


    public override void CardSetup(BasePiece basePiece, CardPowerManager _cardPowerManager)
    {
        mKing = basePiece;
        cardPowerManager = _cardPowerManager;
        mMovement = mCardSO._CardPoweraArea;  // 0.3.0
        mColor = Color.white;
    }

    public override void CreateCellPath(int xDirection, int yDirection, int movement)
    {
        int currentX = mCurrentCell.mBoardPosition.x;
        int currentY = mCurrentCell.mBoardPosition.y;

        CheckPaths(xDirection, yDirection, movement,  currentX,  currentY);
        CheckPaths(xDirection, yDirection, movement,  currentX+1,  currentY);
        CheckPaths(xDirection, yDirection, movement,  currentX-1,  currentY);

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

            if (cellState == CellState.Free)
                mHighlightedCells.Add(mCurrentCell.mBoard.mAllCells[currentX, currentY]);
        }
    }
        public override void CheckPathing()
    {
        CreateCellPath(0, 1, mMovement.y);
    }


   

}
