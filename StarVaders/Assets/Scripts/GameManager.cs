using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Board mBoard;
    public PieceManager mPieceManager;
    public EnemySpawner mEnemySpawner;
    private void Awake()
    {
        mEnemySpawner = GetComponent<EnemySpawner>();
        mBoard.Create();
        mEnemySpawner.GetBP(mPieceManager, mBoard);
    }
    void Start()
    {
        mPieceManager.Setup(mBoard);
    }
}
