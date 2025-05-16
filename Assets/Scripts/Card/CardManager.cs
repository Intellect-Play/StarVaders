using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{

    public static CardManager Instance;
    public CardManagerMove cardManagerMove;
    [SerializeField]private CardPowerManager powerManager;
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
    }
    private void Start()
    {
        powerManager = GetComponent<CardPowerManager>();
    }

    public void ExitTurnButton()
    {
        cardManagerMove.ReturnAllCards();
    }
}
