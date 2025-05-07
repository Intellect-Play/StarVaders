using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Board mBoard;
    public PieceManager mPieceManager;
    public EnemySpawner mEnemySpawner;
    public CardPowerManager mCardPowerManager;
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
        mEnemySpawner = GetComponent<EnemySpawner>();
        mBoard.Create();
        mEnemySpawner.GetBP(mPieceManager, mBoard);
        mPieceManager.board = mBoard;
        mPieceManager.Setup(mBoard);
        mCardPowerManager.mKing = mPieceManager.mWhitePieces[0];
    }
    void Start()
    {
        

    }
}
