using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Card;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(CardClick))]

public class Meteor5x : BasePiece , ICard
{
    public BasePiece mKing;
    [SerializeField] private CardType cardType=CardType.DoubleFire;
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
        BombAreas = GetRandomCells(GameManager.Instance.mBoard.mAllCells);
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
    public List<Cell> GetRandomCells(Cell[,] mAllCells, int count = 5)
    {
        List<Cell> allCells = new List<Cell>();

        int width = mAllCells.GetLength(0);
        int height = mAllCells.GetLength(1);

        // Bütün hüceyrələri listə əlavə edək
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                allCells.Add(mAllCells[x, y]);
            }
        }

        // Random qarışdır və ilk `count` qədər götür
        System.Random rng = new System.Random();
        return allCells.OrderBy(c => rng.Next()).Take(count).ToList();
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
