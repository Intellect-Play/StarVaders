using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Card;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(CardClick))]

public class Bomb_Double : BasePiece , ICard
{
    public BasePiece mKing;
    private CardType cardType=CardType.BombDouble;
    public CardType _CardType => cardType;

    public CardPowerManager cardPowerManager;
    public List<Cell> Enemies = new List<Cell>();
    public List<Cell> BombAreas = new List<Cell>();

    Cell cell;

    private void Start()
    {
        cardPowerManager=GameManager.Instance.mCardPowerManager;
        cardPowerManager.mCards.Add(this);
    }
    public void CardSetup(BasePiece basePiece, CardPowerManager _cardPowerManager)
    {
        mKing = basePiece;
        cardPowerManager = _cardPowerManager;
        mMovement = new Vector3Int(0, 15, 0);
        mColor = Color.white;
        BombAreas = GetRandom2x2Cells(GameManager.Instance.mBoard.mAllCells);
    }


    private void CheckPaths()
    {
        for (int i = 0; i < BombAreas.Count; i++)
        {

            cell = BombAreas[i];
            CellState cellState = CellState.None;
            cellState = mCurrentCell.mBoard.ValidateCellforCards(cell.mBoardPosition.x, cell.mBoardPosition.y, this);

            if (cellState == CellState.Enemy)
            {

                Enemies.Add(cell);
                mHighlightedCells.Add(cell);
                continue;
            }

            if (cellState != CellState.Free)
                continue;

            mHighlightedCells.Add(cell);
        }
    }
    public override void CheckPathing()
    {
        CheckPaths();
    }
    public List<Cell> GetRandom2x2Cells(Cell[,] mAllCells, int count = 3)
    {
        List<Cell> result = new List<Cell>();
        HashSet<Vector2Int> usedPositions = new HashSet<Vector2Int>();

        int maxX = Board.cellX - 1;
        int maxY = Board.cellY - 1;

        System.Random rng = new System.Random();

        int attempts = 0;
        while (result.Count < count * 4 && attempts < 200)
        {
            int x = rng.Next(0, maxX);
            int y = rng.Next(0, maxY);

            // 2x2 blokdakı bütün hüceyrələrin koordinatları
            Vector2Int[] positions =
            {
            new Vector2Int(x, y),
            new Vector2Int(x + 1, y),
            new Vector2Int(x, y + 1),
            new Vector2Int(x + 1, y + 1)
        };

            // Əgər hər hansı biri artıq seçilibsə, bu kvadratı keç
            if (positions.Any(pos => usedPositions.Contains(pos)))
            {
                attempts++;
                continue;
            }

            // Əlavə et
            foreach (var pos in positions)
            {
                usedPositions.Add(pos);
                result.Add(mAllCells[pos.x, pos.y]);
            }
        }

        return result;
    }
    public override void ShowCells()
    {
        foreach (Cell cell in BombAreas)
            cell.mOutlineImage.enabled = true;
    }
    public override void ClearCells()
    {
        foreach (Cell cell in BombAreas)
            cell.mOutlineImage.enabled = false;

        mHighlightedCells.Clear();
    }

    #region CardControlsWithManager
    public void SelectedCard()
    {
        mCurrentCell = mKing.mCurrentCell;
        ShowCells();
        CheckPathing();
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
        cardPowerManager.GetICard(this);
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
    #endregion

}
