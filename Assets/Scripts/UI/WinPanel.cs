using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Mygame.MyUI
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField] Text tipsText;
        [SerializeField] Text scoreText;
        [SerializeField] Text rankingText;
        public void GameWin(GameType gameType, float score)
        {
            if (gameType == GameType.Racing)
            {
                scoreText.text = score.ToString();
                tipsText.text = "您的得分为";
            }
            else if (gameType == GameType.Normal)
            {
                scoreText.text = (score * 100).ToString("F1") + "%";
                tipsText.text = "您的正确率为";
                //TODO: 获取排行榜
            }
        }
    }
}