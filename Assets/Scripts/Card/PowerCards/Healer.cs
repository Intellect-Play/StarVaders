using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : CardBase
{
    public override CardType _CardType => CardType.Healer;

    public override void CheckPathing()
    {
      
    }


    public override void UseCard()
    {
        if(PieceManager.Instance.mWhitePiece != null) PieceManager.Instance.mWhitePiece.Heal.SetActive(true);
        GameManager.Instance.ChangeHealth(-1); // Example healing amount
        // Heal the player
        //Health.HealthPlayer += 10; // Example healing amount
    }
}
