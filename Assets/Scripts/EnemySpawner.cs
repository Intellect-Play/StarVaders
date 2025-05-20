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
        //WaveManager.Instance.StartLevel(1);
        mPieceManager.SetupNewEnemies(GetRandomPieceType(), Random.Range(1,5));
    }
    public void SpawnEnemy(string type, int column)
    {
        Debug.Log("SpawnEnemy");
        mPieceManager.SetupNewEnemies(type, column);
    }
    public void SpawnWave(List<EnemyData> enemies)
    {
        foreach (var enemy in enemies)
        {
            Debug.Log($"Spawning enemy: {enemy.type} in column: {enemy.column}");
            SpawnEnemy(enemy.type, enemy.column);
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