using System.Collections;
using System.Collections.Generic;
using Mygame.Card;
using UnityEngine;
namespace Mygame.MyUI
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] RectTransform selectImage;
        [SerializeField] GameObject mainPanel;
        [SerializeField] GameObject menuPanel;
        [SerializeField] GameObject generatePanel;
        public bool isGaming;
        [SerializeField] GameObject pausePanel;
        [SerializeField] WinPanel winPanel;
        private void Start()
        {
            menuPanel.SetActive(true);
            pausePanel.SetActive(false);
            generatePanel.SetActive(false);
            winPanel.gameObject.SetActive(false);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ShowPausePanel();
            }
        }
        public void SelectButton(Vector3 pos, bool isSelect)
        {
            if (isSelect)
            {
                selectImage.gameObject.SetActive(true);
                selectImage.position = pos;
            }
            else
            {
                selectImage.gameObject.SetActive(false);
            }
        }
        /// <summary>
        /// 显示暂停菜单
        /// </summary>
        public void ShowPausePanel()
        {
            if (isGaming)
            {
                if (pausePanel.activeSelf)
                {
                    pausePanel.SetActive(false);
                    GameManager.Instance.PauseGame(false);
                }
                else if (!winPanel.gameObject.activeSelf)
                {
                    pausePanel.SetActive(true);
                    GameManager.Instance.PauseGame(true);
                }
            }
        }
        /// <summary>
        /// 进入普通模式
        /// </summary>
        public void EnterNormalMode(int x, int y, int PairNum)
        {
            menuPanel.SetActive(false);
            generatePanel.SetActive(false);
            GameManager.Instance.gameType = GameType.Normal;
            isGaming = true;
            CardManager.Instance.GenerateCard(x, y, PairNum);
        }
        /// <summary>
        /// 进入竞速模式
        /// </summary>
        public void EnterRacingMode()
        {
            menuPanel.SetActive(false);
            generatePanel.SetActive(false);
            GameManager.Instance.gameType = GameType.Racing;
            isGaming = true;
        }
        /// <summary>
        /// 重新开始
        /// </summary>
        public void ResetGame()
        {
            GameManager.Instance.PauseGame(false);
            GameManager.Instance.ResetGame();
            pausePanel.SetActive(false);
            winPanel.gameObject.SetActive(false);
        }
        /// <summary>
        /// 继续游戏
        /// </summary>
        public void ContinueGame()
        {
            GameManager.Instance.PauseGame(false);
            pausePanel.SetActive(false);
        }
        /// <summary>
        /// 回到主菜单
        /// </summary>
        public void EnterMenu()
        {
            menuPanel.SetActive(true);
            generatePanel.SetActive(false);
            pausePanel.SetActive(false);
            winPanel.gameObject.SetActive(false);
            GameManager.Instance.EnterMenu();
            GameManager.Instance.PauseGame(false);
        }
        /// <summary>
        /// 退出游戏
        /// </summary>
        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif

            Application.Quit();
        }
        public void GameWin(float time, float score)
        {
            StartCoroutine(GameWinIE(time, score));
        }
        IEnumerator GameWinIE(float time, float score)
        {
            yield return new WaitForSeconds(time);
            winPanel.gameObject.SetActive(true);
            winPanel.GameWin(GameManager.Instance.gameType, score);
        }

    }
}