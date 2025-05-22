using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    Button endTurnButton;
    private void Start()
    {
        endTurnButton = GetComponent<Button>();
        endTurnButton.onClick.AddListener(StartBattle);
    }

    void StartBattle() { 
    
        GameManagerMain.Instance.BattleStartInMainMenu(true);

    }
}
