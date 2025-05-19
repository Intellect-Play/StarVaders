using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public abstract class BasePiece : MonoBehaviour
{
    public Color mColor = Color.clear;
    public bool mIsFirstMove = true;

    [SerializeField] public Cell mOriginalCell = null;
    [SerializeField] public Cell mCurrentCell = null;

    public RectTransform mRectTransform = null;
    public PieceManager mPieceManager;

    public Cell mTargetCell = null;
    public Vector3Int mMovement = Vector3Int.one;
    public List<Cell> mHighlightedCells = new List<Cell>();

    public bool down=true;

    public bool moveCard=false;
    public virtual void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        down = true;
        mPieceManager = newPieceManager;
        mColor = newTeamColor;
        mRectTransform = GetComponent<RectTransform>();
    }

    public virtual void Place(Cell newCell)
    {
        mCurrentCell = newCell;
        mOriginalCell = newCell;
        mCurrentCell.mCurrentPiece = this;
        transform.position = newCell.transform.position;
        gameObject.SetActive(true);
    }

    public void ResetKill()
    {
        Kill();
        mIsFirstMove = true;
        Place(mOriginalCell);
    }

    public virtual void Kill()
    {
        if (mCurrentCell != null)
            mCurrentCell.mCurrentPiece = null;
        gameObject.SetActive(false);
    }

    public bool HasMove()
    {
        CheckPathing();
        return mHighlightedCells.Count > 0;
    }

    public void ComputerMove(int moveDistance, bool _down)
    {
        try
        {
            down = _down;
            if (!HasMove()) return;
                //    continue;
            CheckEnemyKillCase();
            ClearCells();
            CheckPathing();
            mTargetCell = mHighlightedCells[moveDistance];
            Move();
        }
        catch { }
    }

    private void CheckEnemyKillCase()
    {
        if (mColor == Color.black && mCurrentCell.mBoardPosition.y == 0)
            Kill();
    }

    #region Movement
    public virtual void CreateCellPath(int xDirection, int yDirection, int movement)
    {
        int currentX = mCurrentCell.mBoardPosition.x;
        int currentY = mCurrentCell.mBoardPosition.y;

        for (int i = 1; i <= movement; i++)
        {
            currentX += xDirection;
            currentY += yDirection;

            CellState cellState = mCurrentCell.mBoard.ValidateCell(currentX, currentY, this);

            if (cellState == CellState.Enemy)
            {
                mHighlightedCells.Add(mCurrentCell.mBoard.mAllCells[currentX, currentY]);
                break;
            }

            if (cellState != CellState.Free)
                break;

            mHighlightedCells.Add(mCurrentCell.mBoard.mAllCells[currentX, currentY]);
        }
    }

    public virtual void CheckPathing()
    {
        CreateCellPath(1, 0, mMovement.x);
        CreateCellPath(-1, 0, mMovement.x);
        CreateCellPath(0, 1, mMovement.y);
        CreateCellPath(0, -1, mMovement.y);
        CreateCellPath(1, 1, mMovement.z);
        CreateCellPath(-1, 1, mMovement.z);
        CreateCellPath(-1, -1, mMovement.z);
        CreateCellPath(1, -1, mMovement.z);
    }

    public virtual void ShowCells()
    {
        foreach (Cell cell in mHighlightedCells)
            cell.mOutlineImage.enabled = true;
    }

    public virtual void ClearCells()
    {
        foreach (Cell cell in mHighlightedCells)
            cell.mOutlineImage.enabled = false;

        mHighlightedCells.Clear();
    }

    public virtual void Move()
    {
        if (mColor == Color.white)
        {
            CardManagerMove.MoveCard = false;

        }

        mIsFirstMove = false;

        mTargetCell.RemovePiece();
        mCurrentCell.mCurrentPiece = null;

        mCurrentCell = mTargetCell;
        mCurrentCell.mCurrentPiece = this;

        transform.position = mCurrentCell.transform.position;
        mTargetCell = null;
    }
    #endregion

  
}
