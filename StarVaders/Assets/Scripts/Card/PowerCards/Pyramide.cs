using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Card;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CardClick))]

public class Pyramide : BasePiece , ICard
{
    public BasePiece mKing;
    private CardType cardType=CardType.Pyramide;
    public CardType _CardType => cardType;

    public CardPowerManager cardPowerManager;
    public List<Cell> Enemies = new List<Cell>();
    Cell cell;

    private void Start()
    {
        cardPowerManager = GameManager.Instance.mCardPowerManager;
        cardPowerManager.mCards.Add(this);
    }
    public void CardSetup(BasePiece basePiece, CardPowerManager _cardPowerManager)
    {
        mKing = basePiece;
        cardPowerManager = _cardPowerManager;
        mMovement = new Vector3Int(5, 0, 0);
        mColor = Color.white;
    }

    public override void CreateCellPath(int xDirection, int yDirection, int movement)
    {
        int currentX = mCurrentCell.mBoardPosition.x-3;
        int currentY = mCurrentCell.mBoardPosition.y;
        CheckPaths(xDirection, yDirection, movement,  currentX,  currentY+1);
        CheckPaths(xDirection, yDirection, movement-2, currentX+1, currentY + 2);
        CheckPaths(xDirection, yDirection, movement-4, currentX+2, currentY + 3);

        //CheckPaths(xDirection, yDirection, movement,  currentX,  currentY);
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
                mHighlightedCells.Add(cell);
                continue;
            }

            if (cellState != CellState.Free)
                continue;

            mHighlightedCells.Add(mCurrentCell.mBoard.mAllCells[currentX, currentY]);
        }
    }
    public override void CheckPathing()
    {
        CreateCellPath(1, 0, mMovement.x);
        //CreateCellPath(-1, 0, mMovement.x);

    }
    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    ClickGiveManagerSelectedCard();
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    UseCard();
    //    ExitCard();
    //}

    #region CardControlsWithManager
    public void SelectedCard()
    {
        mCurrentCell = mKing.mCurrentCell;
        CheckPathing();
        ShowCells();
    }
    public void UseCard()
    {
        foreach(Cell c in Enemies)
        {
            c.RemovePiece();
        }
    }
  
    public void ExitCard()
    {
        ClearCells();
        Enemies.Clear();
    }
    #endregion

    #region Click
    public void ClickGiveManagerSelectedCard()
    {
        Debug.Log("ClickGiveManagerSelectedCard");
        cardPowerManager.GetICard(this);
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
    #endregion

}
