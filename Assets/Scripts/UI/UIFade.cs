using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mygame.MyUI
{
    public class UIFade : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;

        public float fadeTime = 0.3f;
        private Coroutine fadeCoroutine;
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
        }
        private void OnEnable()
        {
            FadeIn(null);
        }
        public void FadeIn(Action callback)
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeInIE(callback));
        }
        IEnumerator FadeInIE(Action callback)
        {
            float currentTime = 0;
            canvasGroup.alpha = 0;
            rectTransform.localScale = Vector3.zero;
            while (currentTime < fadeTime)
            {
                canvasGroup.alpha = currentTime / fadeTime;
                rectTransform.localScale = Vector3.one * currentTime / fadeTime;
                currentTime += Time.deltaTime;
                yield return null;
            }
            canvasGroup.alpha = 1f;
            rectTransform.localScale = Vector3.one;
            callback?.Invoke();
        }
        public void FadeOut(Action callback)
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeOutIE(callback));
        }
        IEnumerator FadeOutIE(Action callback)
        {
            float currentTime = fadeTime;
            canvasGroup.alpha = 1f;
            rectTransform.localScale = Vector3.one;
            while (currentTime > 0)
            {
                canvasGroup.alpha = currentTime / fadeTime;
                rectTransform.localScale = Vector3.one * currentTime / fadeTime;
                currentTime -= Time.deltaTime;
                yield return null;
            }
            callback?.Invoke();
            canvasGroup.alpha = 0;
            rectTransform.localScale = Vector3.zero;
        }
    }
}