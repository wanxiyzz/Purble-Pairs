using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mygame.Card
{
    public class GameCard : MonoBehaviour
    {
        public int cardTypeIndex;
        public bool isOpen;//是否翻开
        public bool isPair;//是否配对
        public bool isTurnOver;//是否彻底翻开
        private Coroutine turnupCoroutine;//翻转协程
        [SerializeField] Renderer frnotRederer;
        [SerializeField] Renderer cardRenderer;
        private Coroutine cardMoveCoroutine;
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(int typeIndex, bool isPair)
        {
            if (isPair)
            {
                this.isPair = true;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                this.isOpen = true;
            }
            this.cardTypeIndex = typeIndex;
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            frnotRederer.material = CardManager.Instance.cardMaterials[cardTypeIndex];
        }
        /// <summary>
        /// 翻开
        /// </summary>
        public void OpenCard()
        {
            isOpen = true;
            if (turnupCoroutine != null) StopCoroutine(turnupCoroutine);
            turnupCoroutine = StartCoroutine(OpenCardIE());
        }
        IEnumerator OpenCardIE()
        {
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.identity;
            float currentTime = 0;
            while (currentTime < Setting.turnupTime)
            {
                currentTime += Time.deltaTime;
                float t = Mathf.Clamp01(currentTime / Setting.turnupTime);
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
                yield return null;
            }
            transform.rotation = endRotation;
            isTurnOver = true;
        }
        /// <summary>
        /// 合上
        /// </summary>
        public void CloseCard()
        {
            isTurnOver = false;
            if (CardManager.Instance.lastCard == this) CardManager.Instance.lastCard = null;//将当前牌设为空
            if (turnupCoroutine != null) StopCoroutine(turnupCoroutine); turnupCoroutine = StartCoroutine(CloseCardIE());
        }
        IEnumerator CloseCardIE()
        {
            Quaternion startRotation = transform.rotation;
            Quaternion endRotation = Quaternion.Euler(0, 180, 0);
            float currentTime = 0;
            while (currentTime < Setting.turnupTime)
            {
                currentTime += Time.deltaTime;
                float t = Mathf.Clamp01(currentTime / Setting.turnupTime);
                transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
                yield return null;
            }
            transform.rotation = endRotation;
            isOpen = false;
        }
        public void CardMove(Vector3 targetPos, float time)
        {
            if (cardMoveCoroutine != null) StopCoroutine(cardMoveCoroutine);
            cardMoveCoroutine = StartCoroutine(CardMoveIE(targetPos, time));
        }
        IEnumerator CardMoveIE(Vector3 targetPos, float time)
        {
            Vector3 startPos = transform.position;
            float currentTime = 0;
            while (currentTime < time)
            {
                currentTime += Time.deltaTime;
                float t = Mathf.Clamp01(currentTime / time);
                transform.position = Vector3.Lerp(startPos, targetPos, t);
                yield return null;
            }
        }

        public void IsSelected(bool isSelected)
        {
            cardRenderer.material.color = isSelected ? Color.green : Color.white;
        }
        public void PairCards()
        {
            StartCoroutine(PairCardsIE());
        }
        IEnumerator PairCardsIE()
        {
            yield return new WaitForSeconds(Setting.turnupTime);
            isPair = true;
            Vector3 targetScale = new Vector3(1.2f, 1.2f, 1f);
            float changeTime = 0.2f;
            float time = 0f;
            while (time < changeTime)
            {
                time += Time.deltaTime;
                transform.localScale = Vector3.Lerp(Vector3.one, targetScale, Mathf.Clamp01(time / changeTime));
                yield return null;
            }
            time = 0f;
            while (time < changeTime)
            {
                time += Time.deltaTime;
                transform.localScale = Vector3.Lerp(targetScale, Vector3.one, Mathf.Clamp01(time / changeTime));
                yield return null;
            }
            frnotRederer.material.color = Setting.turnupColor;
        }
    }
}