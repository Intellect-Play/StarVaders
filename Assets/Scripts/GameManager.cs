using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Board mBoard;
    public PieceManager mPieceManager;
    public EnemySpawner mEnemySpawner;
    public CardPowerManager mCardPowerManager;
    public Health mHealth;
    public Coin mCoin;
    bool cardMove;
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
        cardMove = true;
        mEnemySpawner = GetComponent<EnemySpawner>();
        mHealth = GetComponent<Health>();
        mCoin = GetComponent<Coin>();
        mBoard.Create();
        mEnemySpawner.GetBP(mPieceManager, mBoard);
        mPieceManager.board = mBoard;
        mPieceManager.Setup(mBoard);
        mCardPowerManager.mKing = mPieceManager.mWhitePiece;
    }
    public void EndTurn()
    {
        mPieceManager.SwitchSides(Color.white);
        WaveManager.Instance.SpawnNextWave();
        //mEnemySpawner.EnemySpawnF();
        mEnemySpawner.EnemyMoveF();
    }

    public void ChangeHealth(int heath)
    {
        mHealth.TakeDamage(heath);
        GameUI.Instance.UpdateHealth(mHealth.HealthPlayer);
    }

    public void ChangeCoin(int coin)
    {
        mCoin.AddCoin(coin);
        GameUI.Instance.UpdateScore(mCoin.CoinPlayer);
    }

    public void WinGame()
    {
        SaveManager.Instance.saveData.playerData.coins += mCoin.CoinPlayer;
        SaveManager.Instance.saveData.playerData.currentLevel += 1;
        SaveManager.Instance.Save();
        GameUI.Instance.WinGame();
    }

    public void LoseGame()
    {
        SaveManager.Instance.saveData.playerData.coins += mCoin.CoinPlayer;
        SaveManager.Instance.Save();
        GameUI.Instance.LoseGame();
    }

    public void EndTurnButton()
    {
        if (!cardMove) return;
        StartCoroutine(WaitForEndTurn());
    }
    IEnumerator WaitForEndTurn()
    {
        cardMove = false;
        yield return new WaitForSeconds(1);
        //CardManager.Instance.ExitTurnButton();
        GameManager.Instance.EndTurn();
        //CardManager.Instance.cardManagerMove.ReturnAllCards();
        yield return new WaitForSeconds(1);
        CardManager.Instance.cardManagerMove.SpawnCards();
        //yield return new WaitForSeconds(1);
        cardMove = true;


    }
}
