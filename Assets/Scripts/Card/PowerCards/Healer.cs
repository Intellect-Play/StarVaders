using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : CardBase
{
    public override CardType _CardType => CardType.Healer;

    public override void CheckPathing()
    {
        throw new System.NotImplementedException();
    }

    public override void CardSetup(BasePiece basePiece, CardPowerManager _cardPowerManager)
    {
        
    }
    public override void SelectedCard()
    {
    }
    public override void ExitCard()
    {
    }
    public override void UseCard()
    {
        // Heal the player
        Health.HealthPlayer += 10; // Example healing amount
    }
}
