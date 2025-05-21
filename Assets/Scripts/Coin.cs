using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int CoinPlayer;
    private void Start()
    {
        CoinPlayer = SaveManager.Instance.saveData.playerData.coins;
    }
    public void AddCoin(int damage)
    {
        CoinPlayer += damage;
    }
}
