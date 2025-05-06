using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public PieceManager mPieceManager;
    public Board mBoard;
    private string[] mPieceOrder = new string[2]
  {
        "KN", "B"////, "P" ,"Q",   "R"
  };
    public void GetBP(PieceManager pieceManager, Board board)
    {
        mPieceManager = pieceManager;
        mBoard = board;
    }

    public void EnemyMoveF()
    {
       mPieceManager.EnemyMove();
    }
    public void EnemySpawnF()
    {
        mPieceManager.SetupNewEnemies(GetRandomPieceType(), Random.Range(1,4));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mPieceManager.SetupNewEnemies(GetRandomPieceType(), Random.Range(1, 5));
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            mPieceManager.EnemyMove();
        }
    }
    string GetRandomPieceType()
    {
        int randomIndex = Random.Range(0, mPieceOrder.Length);
        return mPieceOrder[randomIndex];
    }
}
