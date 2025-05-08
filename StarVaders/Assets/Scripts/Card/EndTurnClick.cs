using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnClick : MonoBehaviour
{
    public void EndTurnButton()
    {
        CardManager.Instance.ExitTurnButton();
        GameManager.Instance.EndTurn();
    }
}
