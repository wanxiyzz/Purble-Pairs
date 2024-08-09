using System.Collections;
using Mygame.MyUI;
using UnityEngine;
namespace Mygame.Card
{
    public class CardManager : Singleton<CardManager>
    {
        public Material[] cardMaterials;
        public GameCard[] cards;
        [SerializeField] private int openCardCount = 0;
        private Coroutine CloseAllCardsIECorotine;
        public GameCard lastCard;


        //游戏开始
        private GameCard[] openCards;
        private int allPairNum;
        private int currentPairNum;
        private int rows;
        private int cols;
        private int cardNeedNum;


        private int successCount;
        private int failCount;
        protected override void Awake()
        {
            base.Awake();
            LoadAllMaterials();
        }
        IEnumerator Start()
        {
            yield return new WaitForSeconds(1f);
        }
        public void ClickCard(GameCard card)
        {
            if (lastCard == null || lastCard.isTurnOver)
            {
                if (!card.isOpen) SelectCard(card);
            }
        }
        private void SelectCard(GameCard card)
        {
            if (openCardCount < cardNeedNum)
            {
                card.OpenCard();
                openCards[openCardCount] = card;
                openCardCount++;
                if (openCardCount == cardNeedNum)
                {
                    lastCard = card;
                    for (int i = 1; i < openCardCount; i++)
                    {
                        if (openCards[i].cardTypeIndex == openCards[i - 1].cardTypeIndex)
                        {
                            continue;
                        }
                        else//配对失败
                        {
                            CloseAllCardsIECorotine = StartCoroutine(CloseAllCardsIE());
                            failCount++;
                            return;
                        }
                    }
                    PairCards();
                }
            }
            else
            {
                CloseAllCards();
                card.OpenCard();
                openCards[openCardCount] = card;
                openCardCount++;
            }
        }
        /// <summary>
        /// 直接关闭卡片
        /// </summary>
        private void CloseAllCards()
        {
            if (CloseAllCardsIECorotine != null) StopCoroutine(CloseAllCardsIECorotine);
            for (int i = 0; i < openCardCount; i++)
            {
                openCards[i]?.CloseCard();
                openCards[i] = null;
            }
            openCardCount = 0;
        }
        /// <summary>
        /// 等时间自动关闭卡片
        /// </summary>
        private IEnumerator CloseAllCardsIE()
        {
            yield return new WaitForSeconds(Setting.openingTime);
            CloseAllCards();
        }
        /// <summary>
        /// 在文件中自动加载生成卡片图案
        /// </summary>
        private void LoadAllMaterials()
        {
            string materialsFolderPath = "Materials";
            cardMaterials = Resources.LoadAll<Material>(materialsFolderPath);
        }
        /// <summary>
        /// 生成卡片
        /// </summary>
        /// <param name="x">列数</param>
        /// <param name="y">行数</param>
        /// <param name="distance">距离</param>
        public void GenerateCard(int x, int y, int PairNum, float distance = 2.5f)
        {

            cardNeedNum = PairNum; //每一组卡片需要的图案数量

            openCards = new GameCard[cardNeedNum];//打开的卡片

            allPairNum = x * y / cardNeedNum; //计算卡片对数

            int cardNum = allPairNum * cardNeedNum;//卡牌的总数.

            cols = x;
            rows = y;

            cards = new GameCard[cardNum];
            for (int i = 0; i < cardNum; i++)
            {
                cards[i] = CardPool.Instance.cardPool.PrepareObject();
            }
            MoveCards(distance);
            SetAllCardMaterial();
        }
        /// <summary>
        /// 发牌
        /// </summary>
        public void MoveCards(float distance = 2.5f)
        {
            float xOffset = (cols - 1) * distance / 2.0f;
            float yOffset = (rows - 1) * distance / 2.0f;
            SetCameraSize(xOffset, yOffset, distance);
            int max = cards.Length - 1;//最大索引
            Debug.Log(cards.Length);
            int num = 0;
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < cols; i++)
                {
                    float xPos = i * distance - xOffset;
                    float yPos = -j * distance + yOffset;
                    cards[num].CardMove(new Vector3(xPos, yPos, 0), 0.5f);
                    if (num >= max) break;
                    num++;
                }
            }
        }
        /// <summary>
        /// 设置卡牌材质
        /// </summary>
        public void SetAllCardMaterial()
        {
            Tools.Disrupt<GameCard>(cards, 20);
            for (int i = 0; i < cards.Length; i++)
            {
                int n = i / cardNeedNum;//材质编号
                if (n > cardMaterials.Length - 1)
                {
                    n = n % cardMaterials.Length;
                }
                cards[i].Init(n, false);
            }
        }
        private void SetCameraSize(float x, float y, float distance)
        {
            float xOffset = (x + 0.5f + distance / 2) * 9 / 16; //根据屏幕宽高比调整相机大小
            float yOffset = y + 0.5f + distance / 2;
            Camera.main.orthographicSize = Mathf.Max(xOffset, yOffset);
        }
        /// <summary>
        /// 配对成功
        /// </summary>
        private void PairCards()
        {
            currentPairNum += 1;
            successCount += 1;
            if (currentPairNum == allPairNum)
            {
                //TODO:游戏胜利
                float successRate = (float)currentPairNum / (currentPairNum + failCount);
                Debug.Log(successRate);
                UIManager.Instance.GameWin(0.5f, successRate);
                //TODO:加入排行榜
            }
            for (int i = 0; i < openCards.Length; i++)
            {
                //TODO:配对成功音效
                openCards[i].PairCards();
                openCards[i] = null;
            }
            openCardCount = 0;
            lastCard = null;
        }
        /// <summary>
        /// 洗牌
        /// </summary>
        public void ShuffleCards()
        {
            StartCoroutine(ShuffleCardsIE());
        }
        IEnumerator ShuffleCardsIE()
        {
            Tools.Disrupt<GameCard>(cards, 20);
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].CloseCard();
                cards[i].CardMove(Vector3.zero, 0.5f);
            }
            yield return new WaitForSeconds(0.5f);
            MoveCards();
        }
        public void RemmoveAllCards()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                cards[i].gameObject.SetActive(false);
                cards[i].transform.position = Vector3.zero;
            }
            cards = null;
        }

    }
}