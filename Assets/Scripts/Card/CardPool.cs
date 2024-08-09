using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mygame.Card
{
    public class CardPool : Singleton<CardPool>
    {
        public ObjectPool<GameCard> cardPool;
        private void Start()
        {
            cardPool.Initialize(transform);
        }
    }
}