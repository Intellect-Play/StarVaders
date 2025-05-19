using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    public PieceManager mPieceManager;
    public Board mBoard;
    Enemies[] values;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        values = (Enemies[])System.Enum.GetValues(typeof(Enemies));

    }
    public void GetBP(PieceManager pieceManager, Board board)
    {
        mPieceManager = pieceManager;
        mBoard = board;
    }

    public void EnemyMoveF()
    {
       mPieceManager.EnemyMove(0, true);
    }
    public void EnemyBackMoveF()
    {
        mPieceManager.EnemyMove(0,false);
        mPieceManager.EnemyMove(0, false);

    }
    public void EnemySpawnF()
    {
        mPieceManager.SetupNewEnemies(GetRandomPieceType(), Random.Range(1,5));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mPieceManager.SetupNewEnemies(GetRandomPieceType(), Random.Range(1, 5));
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            mPieceManager.EnemyMove(0,true);
        }
    }
    string GetRandomPieceType()
    {
        Enemies randomEnemy = values[Random.Range(0, values.Length)];
        return randomEnemy.ToString();
    }

}
enum Enemies
{
    Queen,
    Bishop,
    Pawn,
    Rock
}