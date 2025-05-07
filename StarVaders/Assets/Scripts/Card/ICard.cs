using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Card
{
    public interface ICard
    {
        GameObject GetGameObject();
        CardType _CardType { get; }
        public void ClickGiveManagerSelectedCard();
        public void SelectedCard();
        public void CardSetup(BasePiece basePiece, CardPowerManager cardPowerManager);
        public void UseCard();
        public void ExitCard();

    }


}
