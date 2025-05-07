using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Card;
using UnityEngine;

public class CardPowerManager : BasePiece
{
    public BasePiece mKing;

    public List<ICard> mCards = new List<ICard>();
    public ICard _SelectedCard;


    private void Start()
    {
        SetupCards();
    }
    public void GetICard(ICard card)
    {
        
        if (true)
        {
            if(_SelectedCard!=null) _SelectedCard.ExitCard();
           
            _SelectedCard = card;
            //_SelectedCard.GetGameObject();
            _SelectedCard.SelectedCard();
        }
    }
    public void SetupCards()
    {
        foreach (ICard card in mCards)
        {
            card.CardSetup(mKing, this);
        }
    }
}

public enum CardType
{
    Bomb3x3,
    But,
    DoubleFire,
    LinearFire,
    Pyramide,
    PushBack,
    Move,
    StrongStrike,
    BombDouble,
    PoisonAttack,
    Meteor5x,
    Sword,
    Healer
}