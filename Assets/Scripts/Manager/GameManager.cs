using System.Collections;
using System.Collections.Generic;
using Mygame.Card;
using Mygame.Cursor;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameType gameType;
    public CheckCard checkCard;
    public ClickCard clickCard;
    void Start()
    {
        clickCard = GetComponent<ClickCard>();
#if UNITY_EDITOR
        InitCursorCheck();
#else
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
        InitCursorCheck();
#endif
#endif
    }
    public void InitCursorCheck()
    {
        checkCard = gameObject.AddComponent<CheckCard>();
    }
    public void ResetGame()
    {
        switch (gameType)
        {
            case GameType.Normal:
                ResetNormalMode();
                break;
            case GameType.Racing:
                ResetRacingMode();
                break;
            default:
                break;
        }
    }
    public void PauseGame(bool isPause)
    {
        checkCard.isPause = isPause;
        clickCard.isPause = isPause;
    }
    public void EnterMenu()
    {
        CardManager.Instance.RemmoveAllCards();
    }
    public void ResetNormalMode()
    {
        CardManager.Instance.ShuffleCards();
    }
    public void ResetRacingMode()
    {

    }
}
