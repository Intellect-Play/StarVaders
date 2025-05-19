using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

// New
public enum CellState
{
    None,
    Friendly,
    Enemy,
    Free,
    OutOfBounds
}

public class Board : MonoBehaviour
{
    public GameObject mCellPrefab;
    public CellRow[] cellRows;
    public List<Cell> allCellsInHierarchy;
    [HideInInspector]
    public Cell[,] mAllCells;
    public static int cellX = 5;
    public static int cellY = 9;
    // We create the board here, no surprise
    private void Awake()
    {
        // Create the board
       // Create();
    }
    private void Start()
    {
        // Create the board
        //Create();
    }
    public void Create()
    {
        #region Create
        //SyncFromEditorCells();
        CreateFromChildren();
        //mAllCells = new Cell[cellX, cellY];
        //for (int y = 0; y < cellY; y++)
        //{
        //    for (int x = 0; x < cellX; x++)
        //    {
        //        // Create the cell
        //        GameObject newCell = Instantiate(mCellPrefab, transform);

        //        // Position
        //        RectTransform rectTransform = newCell.GetComponent<RectTransform>();
        //        rectTransform.anchoredPosition = new Vector2((x * 100) - (cellX * 100 / 2) + 50, (y * 100) - (cellY * 100 / 2) + 50);

        //        // Setup
        //        mAllCells[x, y] = newCell.GetComponent<Cell>();
        //        mAllCells[x, y].Setup(new Vector2Int(x, y), this);
        //    }
        //}
        #endregion

        #region Color
        //for (int x = 0; x < cellX; x += 2)
        //{
        //    for (int y = 0; y < cellY; y++)
        //    {
        //        // Offset for every other line
        //        int offset = (y % 2 != 0) ? 0 : 1;
        //        int finalX = x + offset;

        //        // Color
        //        mAllCells[finalX, y].GetComponent<Image>().color = new Color32(230, 220, 187, 255);
        //    }
        //}
        #endregion
    }
    public void CreateFromChildren()
    {
        Debug.Log("CreateFromChildren");
        mAllCells = new Cell[cellX, cellY];

        // Get all children (assumes they are ordered by rows then columns)
        allCellsInHierarchy = GetComponentsInChildren<Cell>().ToList();
        Debug.Log(GetComponentsInChildren<Cell>().Length);
        if (allCellsInHierarchy.Count != cellX * cellY)
        {
            Debug.LogError("Child count does not match board size!");
            return;
        }

        int index = 0;
        for (int y = 0; y < cellY; y++)
        {
            for (int x = 0; x < cellX; x++)
            {
                mAllCells[x, y] = allCellsInHierarchy[index];
                mAllCells[x, y].Setup(new Vector2Int(x, y), this);
                index++;
            }
        }
    }
    public void SyncFromEditorCells()
    {
        mAllCells = new Cell[cellX, cellY];
        for (int y = 0; y < cellRows.Length; y++)
        {
            for (int x = 0; x < cellRows[y].row.Length; x++)
            {
                mAllCells[x, y] = cellRows[y].row[x];
                mAllCells[x, y].Setup(new Vector2Int(x, y), this);
            }
        }
    }

    // New
    public CellState ValidateCell(int targetX, int targetY, BasePiece checkingPiece)
    {
        // Bounds check
        if (targetX < 0 || targetX > cellX-1)
            return CellState.OutOfBounds;

        if (targetY < 0 || targetY > cellY - 1)
            return CellState.OutOfBounds;

        // Get cell
        Cell targetCell = mAllCells[targetX, targetY];

        // If the cell has a piece
        if (targetCell.mCurrentPiece != null)
        {
            // If friendly
            if (checkingPiece.mColor == targetCell.mCurrentPiece.mColor)
                return CellState.Friendly;

            // If enemy
            if (checkingPiece.mColor != targetCell.mCurrentPiece.mColor)
                return CellState.Enemy;
        }

        return CellState.Free;
    }
    public CellState ValidateCellforCards(int targetX, int targetY, BasePiece checkingPiece)
    {
        // Bounds check
        if (targetX < 0 || targetX > cellX - 1)
            return CellState.OutOfBounds;

        if (targetY < 0 || targetY > cellY - 1)
            return CellState.OutOfBounds;
        Cell targetCell = mAllCells[targetX, targetY];

        // Get cell
        if (targetCell.mCurrentPiece != null)
        {
            

            // If enemy
            if (checkingPiece.mColor != targetCell.mCurrentPiece.mColor)
                return CellState.Enemy;
        }

        return CellState.Free;
    }
}
[System.Serializable]
public class CellRow
{
    public Cell[] row;
}
