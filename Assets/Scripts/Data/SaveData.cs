using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
[Serializable]
public class CardData
{
    public int cardTypeIndex;
    public bool isPair;
}
[Serializable]
public class GameSaveData
{
    public int rows;
    public int cols;
    public int currentPairNum;
    public int allPairNum;//所有需要配对的数量
    public List<CardData> cards;
}
