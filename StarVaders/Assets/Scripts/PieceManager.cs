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
    public List<BasePiece> mAllBlackPieces = null;

    public List<BasePiece> mPromotedPieces = new List<BasePiece>();
    Board board;
    private string[] mPieceOrder = new string[6]
    {
        "K","P",  "Q",  "B", "KN", "R"
    };
 
    private Dictionary<string, Type> mPieceLibrary = new Dictionary<string, Type>()
    {
        {"P",  typeof(Pawn)},
        {"R",  typeof(Rook)},
        {"KN", typeof(Knight)},
        {"B",  typeof(Bishop)},
        {"K",  typeof(King)},
        {"Q",  typeof(Queen)}
    };

    public void Setup(Board _board)
    {
        board = _board;
        mWhitePieces = CreatePieces(Color.white, new Color32(80, 124, 159, 255),1, mPieceOrder[0].ToString());

       // mBlackPieces = CreatePieces(Color.black, new Color32(210, 95, 64, 255), countEnemies, 1);
       // mAllBlackPieces.AddRange(mBlackPieces);
        PlacePieces(1, 0, mWhitePieces, board,1);
       // PlacePieces(Board.cellY-2, Board.cellY - 1, mBlackPieces, board,countEnemies-1);

       // SetupNewEnemies();
    }
    public void SetupNewEnemies(string enemyType, int countEnemies)
    {
        Debug.Log(countEnemies+ " countEnemies");
        mBlackPieces = CreatePieces(Color.black, new Color32(210, 95, 64, 255), countEnemies, enemyType);
        mAllBlackPieces.AddRange(mBlackPieces);

        PlacePieces(Board.cellY - 1, Board.cellY - 1, mBlackPieces, board, countEnemies);
        
        //EnemyMove();
    }

    public void EnemyMove()
    {
       // SwitchSides(Color.black);

        for (int i = 0; i < mAllBlackPieces.Count; i++)
        {
            if (!mAllBlackPieces[i].HasMove())
                continue;
            if (mAllBlackPieces[i].gameObject.activeInHierarchy)
                mAllBlackPieces[i].ComputerMove();
        }
        SwitchSides(Color.black);
    }

    private List<BasePiece> CreatePieces(Color teamColor, Color32 spriteColor,int pieceCount,string enemyType)
    {
        List<BasePiece> newPieces = new List<BasePiece>();

        for (int i = 0; i < pieceCount; i++)
        {
            string key = enemyType;
            Type pieceType = mPieceLibrary[key];

            // Create
            BasePiece newPiece = CreatePiece(pieceType, i);
            newPieces.Add(newPiece);

            // Setup
            newPiece.Setup(teamColor, spriteColor, this);
            if (i!=2) {
                
            }
            // Get the type
            
        }

        return newPieces;
    }

    private BasePiece CreatePiece(Type pieceType,int n)
    {
        // Create new object
        GameObject newPieceObject = Instantiate(mPiecePrefab);
        newPieceObject.transform.SetParent(transform);
        newPieceObject.name=n.ToString() + pieceType.Name;
        // Set scale and position
        newPieceObject.transform.localScale = new Vector3(1, 1, 1);
        newPieceObject.transform.localRotation = Quaternion.identity;

        // Store new piece
        BasePiece newPiece = (BasePiece)newPieceObject.AddComponent(pieceType);

        return newPiece;
    }

    private void PlacePieces(int pawnRow, int royaltyRow, List<BasePiece> pieces, Board board,int countPieces)
    {
        for (int i = 0; i < countPieces; i++)
        {

           // Debug.Log("Placing piece: " + i+"  "+pieces[i].gameObject.name);
            // Place pawns    
            pieces[i].Place(board.mAllCells[i+1, pawnRow]);

            // Place royalty
            //pieces[i + Board.cellY].Place(board.mAllCells[i, royaltyRow]);
        }
    }

    private void SetInteractive(List<BasePiece> allPieces, bool value)
    {
        foreach (BasePiece piece in allPieces)
            piece.enabled = value;
    }

    
    private void MoveRandomPiece()
    {
        BasePiece finalPiece = null;

        while (!finalPiece)
        {
            // Get piece
            int i = UnityEngine.Random.Range(0, mAllBlackPieces.Count);
            BasePiece newPiece = mAllBlackPieces[i];

            // Does this piece have any moves?
            if (!newPiece.HasMove())
                continue;

            // Is piece active?
            if (newPiece.gameObject.activeInHierarchy)
                finalPiece = newPiece;
        }

        finalPiece.ComputerMove();
    }
    

    public void SwitchSides(Color color)
    {
        if (!mIsKingAlive)
        {
            // Reset pieces
            ResetPieces();

            // King has risen from the dead
            mIsKingAlive = true;

            // Change color to black, so white can go first again
            color = Color.black;
        }

        bool isBlackTurn = color == Color.white ? true : false;

        // Set team interactivity
        SetInteractive(mWhitePieces, !isBlackTurn);

        // Disable this so player can't move pieces
        SetInteractive(mAllBlackPieces, isBlackTurn);

        // Set promoted interactivity
        foreach (BasePiece piece in mPromotedPieces)
        {
            bool isBlackPiece = piece.mColor != Color.white ? true : false;
            bool isPartOfTeam = isBlackPiece == true ? isBlackTurn : !isBlackTurn;

            piece.enabled = isPartOfTeam;
        }

        // ADDED: Move random piece
        /*
        if (isBlackTurn)
            MoveRandomPiece();
        */
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
            piece.Reset();

        foreach (BasePiece piece in mAllBlackPieces)
            piece.Reset();
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
