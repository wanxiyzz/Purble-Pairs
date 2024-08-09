using System;
using System.Collections;
using System.Collections.Generic;
using Mygame.Card;
using UnityEngine;
using UnityEngine.UI;

namespace Mygame.MyUI
{
    public class GeneratePanel : MonoBehaviour
    {
        [SerializeField] InputField inputRows;
        [SerializeField] InputField inputColumns;
        [SerializeField] InputField inputPairNum;
        [SerializeField] Button confirmButton;
        [SerializeField] Button cancleButton;
        [SerializeField] UIFade uiFade;
        private void OnEnable()
        {
            inputPairNum.text = "2";
            inputColumns.text = "6";
            inputRows.text = "3";
        }
        void Start()
        {
            confirmButton.onClick.AddListener(OnConfirmClick);
            cancleButton.onClick.AddListener(OnCancleClick);
            inputColumns.onValueChanged.AddListener((value) => OnValueChange(value, inputColumns));
            inputRows.onValueChanged.AddListener((value) => OnValueChange(value, inputRows));
        }

        private void OnValueChange(string value, InputField inputField)
        {
            int num;
            if (int.TryParse(value, out num) && num > 99)
            {
                inputField.text = "99";
            }
        }
        public void OnConfirmClick()
        {
            UIManager.Instance.EnterNormalMode(int.Parse(inputColumns.text), int.Parse(inputRows.text), int.Parse(inputPairNum.text));
        }
        public void OnCancleClick()
        {
            uiFade.FadeOut(() => gameObject.SetActive(false));
        }
    }
}