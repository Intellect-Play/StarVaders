using System;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    [HideInInspector]
    public bool mIsKingAlive = true;

    public GameObject mPiecePrefab;

    public List<BasePiece> mWhitePieces = null;
    public List<BasePiece> mBlackPieces = null;
    public List<GameObject> mPiecePrefabs = null;

    public List<BasePiece> mAllBlackPieces = null;

    public List<BasePiece> mPromotedPieces = new List<BasePiece>();
    public Board board;

    GameObject newPieceObject;

    //private Dictionary<string, Type> mPieceLibrary = new Dictionary<string, Type>()
    //{
    //    {"P",  typeof(Pawn)},
    //    {"R",  typeof(Rook)},
    //    {"KN", typeof(Knight)},
    //    {"B",  typeof(Bishop)},
    //    {"K",  typeof(King)},
    //    {"Q",  typeof(Queen)}
    //};

    public void Setup(Board _board)
    {
        board = _board;
        mWhitePieces = CreatePieces(Color.white, new Color32(80, 124, 159, 255),1, "_King");

        PlacePiece(1, 3, mWhitePieces[0]);       
    }
    public void SetupNewEnemies(string enemyType, int posEnemy)
    {
        mBlackPieces = CreatePieces(Color.black, Color.white, posEnemy, enemyType);
        mAllBlackPieces.AddRange(mBlackPieces);

        PlacePieces(Board.cellY - 1,  mBlackPieces, posEnemy);        
    }

    public void EnemyMove(int moveDistance,bool down)
    {
        // SwitchSides(Color.black);
        if (down)
        {
            for (int i = 0; i < mAllBlackPieces.Count; i++)
            {
                //if (!mAllBlackPieces[i].HasMove())
                //    continue;
                if (mAllBlackPieces[i].gameObject.activeInHierarchy)
                    mAllBlackPieces[i].ComputerMove(moveDistance, down);
            }
        }
        else
        {
            for (int i = mAllBlackPieces.Count-1; i >-1; i--)
            {
                //if (!mAllBlackPieces[i].HasMove())
                //    continue;
                if (mAllBlackPieces[i].gameObject.activeInHierarchy)
                    mAllBlackPieces[i].ComputerMove(moveDistance, down);
            }
        }

            SwitchSides(Color.black);
    }

    private List<BasePiece> CreatePieces(Color teamColor, Color32 spriteColor,int pieceCount,string enemyType)
    {
        List<BasePiece> newPieces = new List<BasePiece>();

        for (int i = 0; i < pieceCount; i++)
        {
            string key = enemyType;
           // Type pieceType = mPieceLibrary[key];

            // Create
            BasePiece newPiece = CreatePiece(enemyType, i);
            newPieces.Add(newPiece);

            // Setup
            newPiece.Setup(teamColor, spriteColor, this);
            if (i!=2) {
                
            }
            // Get the type
            
        }

        return newPieces;
    }

    private BasePiece CreatePiece(string pieceType,int n)
    {
        foreach (GameObject piece in mPiecePrefabs)
        {
            if (piece.name == pieceType)
            {
                newPieceObject = Instantiate(piece);

                continue;
            }
        }   
        // Create new object
        newPieceObject.transform.SetParent(transform);
        //newPieceObject.name=n.ToString() + pieceType;
        // Set scale and position
        newPieceObject.transform.localScale = new Vector3(1, 1, 1);
        newPieceObject.transform.localRotation = Quaternion.identity;

        // Store new piece
        BasePiece newPiece = newPieceObject.GetComponent<BasePiece>();

        return newPiece;
    }

    private void PlacePieces(int pawnRow, List<BasePiece> pieces, int countPieces)
    {
        // Mövcud sütunların siyahısı (0, 1, 2, ..., cellX-1)
        List<int> availableColumns = new List<int>();

        for (int x = 0; x < Board.cellX; x++)
        {
            availableColumns.Add(x);
        }

        // Təsadüfi qarışdır
        ShuffleList(availableColumns);

        // countPieces sayda düşməni yerləşdir
        for (int i = 0; i < countPieces; i++)
        {
            int randomX = availableColumns[i]; // təkrarsız x (sütun)
            PlacePiece(pawnRow, randomX, pieces[i]);
        }
    }

    private void PlacePiece(int pawnRow, int x, BasePiece piece)
    {
        piece.Place(board.mAllCells[x, pawnRow]); 
    }

    private void ShuffleList(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
    private void SetInteractive(List<BasePiece> allPieces, bool value)
    {
        foreach (BasePiece piece in allPieces)
            piece.enabled = value;
    }

    
  
    public void SwitchSides(Color color)
    {
        if (!mIsKingAlive)
        {
            ResetPieces();
            mIsKingAlive = true;

            color = Color.black;
        }

        bool isBlackTurn = color == Color.white ? true : false;

        SetInteractive(mWhitePieces, !isBlackTurn);

        SetInteractive(mAllBlackPieces, isBlackTurn);
        foreach (BasePiece piece in mPromotedPieces)
        {
            bool isBlackPiece = piece.mColor != Color.white ? true : false;
            bool isPartOfTeam = isBlackPiece == true ? isBlackTurn : !isBlackTurn;

            piece.enabled = isPartOfTeam;
        }


    }

    public void ResetPieces()
    {
        foreach (BasePiece piece in mPromotedPieces)
        {
            piece.Kill();
            Destroy(piece.gameObject);
        }

        mPromotedPieces.Clear();

        foreach (BasePiece piece in mWhitePieces)
            piece.ResetKill();

        foreach (BasePiece piece in mAllBlackPieces)
            piece.ResetKill();
    }

    public void PromotePiece(Pawn pawn, Cell cell, Color teamColor, Color spriteColor)
    {
        // Kill Pawn
        //pawn.Kill();

        //// Create
        //BasePiece promotedPiece = CreatePiece(typeof(Queen));
        //promotedPiece.Setup(teamColor, spriteColor, this);

        //// Place piece
        //promotedPiece.Place(cell);

        //// Add
        //mPromotedPieces.Add(promotedPiece);
    }
}
