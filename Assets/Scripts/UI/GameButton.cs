using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Mygame.MyUI
{
    public class GameButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        RectTransform rectTransform;
        public Image image;

        void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }
        void Update()
        {

        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            //音效
            UIManager.Instance.SelectButton(rectTransform.position, true);
            image.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.SelectButton(rectTransform.position, false);
            image.gameObject.SetActive(false);
        }

    }
}