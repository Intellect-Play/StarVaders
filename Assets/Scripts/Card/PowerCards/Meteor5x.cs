using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Meteor5x : CardBase
{
    public override CardType _CardType => CardType.Meteor5x;
    List<Cell> randomCells;

    public override void CardSetup(BasePiece basePiece, CardPowerManager _cardPowerManager)
    {
        base.CardSetup(basePiece, _cardPowerManager);
        randomCells = GetRandomCells(GameManager.Instance.mBoard.mAllCells, 5);

    }


    public override void CheckPathing()
    {

        foreach (var cell in randomCells)
        {
            var state = mCurrentCell.mBoard.ValidateCellforCards(cell.mBoardPosition.x, cell.mBoardPosition.y, this);

            if (state == CellState.Enemy)
                Enemies.Add(cell);

            if (state == CellState.Free || state == CellState.Enemy)
                HighlightedCells.Add(cell);
        }
    }

    private List<Cell> GetRandomCells(Cell[,] allCells, int count)
    {
        var flatList = new List<Cell>();
        int width = allCells.GetLength(0);
        int height = allCells.GetLength(1);

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                flatList.Add(allCells[x, y]);

        System.Random rng = new System.Random();
        return flatList.OrderBy(_ => rng.Next()).Take(count).ToList();
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
