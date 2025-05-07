using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Card;
using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(CardClick))]

public class PushBack : BasePiece , ICard
{
    public BasePiece mKing;
    [SerializeField] private CardType cardType=CardType.DoubleFire;
    public CardType _CardType => cardType;

    public CardPowerManager cardPowerManager;
    public List<Cell> Enemies = new List<Cell>();
    Cell cell;

    private void Start()
    {
        cardPowerManager = GameManager.Instance.mCardPowerManager;
        cardPowerManager.mCards.Add(this);
    }
    public void CardSetup(BasePiece basePiece, CardPowerManager _cardPowerManager)
    {
        mKing = basePiece;
        cardPowerManager = _cardPowerManager;
        mMovement = new Vector3Int(0, 15, 0);
        mColor = Color.white;
    }

   


    #region CardControlsWithManager
    public void SelectedCard()
    {
       
    }
    public void UseCard()
    {
        EnemySpawner.Instance.EnemyBackMoveF();
    }
  
    public void ExitCard()
    {
       
    }
    #endregion

    #region Click
    public void ClickGiveManagerSelectedCard()
    {
        cardPowerManager.GetICard(this);
    }
    public GameObject GetGameObject()
    {
        return gameObject;
    }
    #endregion

}
