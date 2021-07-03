using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIController : MonoBehaviour {
    [Header("Container")]
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject inforPanel;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject gameoverPanel;

    [Header("Properties")]
    [SerializeField] CanvasGroup mainCanvasGroup;
    private Vector2 startSettingPos;
    private Vector2 startInforPos;
    private Vector2 startPausePos;
    private Vector2 startGameOverPos;

    private void Awake () {
        startSettingPos = settingPanel.transform.localPosition;
        startInforPos = inforPanel.transform.localPosition;
        startPausePos = pausePanel.transform.localPosition;
        startGameOverPos = gameoverPanel.transform.localPosition;
        mainCanvasGroup.alpha = 0;
    }

    private void Start () {
        GameStateManager.GameStateChanged += GameStateChanged;
    }

    private void GameStateChanged () {
        switch (GameStateManager.CurrentState) {
            case GameState.None:
                break;
            case GameState.Main:
                settingPanel.SetActive(true);
                inforPanel.SetActive(true);
                mainPanel.SetActive(true);
                controlPanel.SetActive(false);
                pausePanel.SetActive(false);
                gameoverPanel.SetActive(false);
                OnMainState();
                break;
            case GameState.Play:
                settingPanel.SetActive(false);
                inforPanel.SetActive(false);
                mainPanel.SetActive(false);
                controlPanel.SetActive(true);
                pausePanel.SetActive(false);
                gameoverPanel.SetActive(false);
                break;
            case GameState.Pause:
                settingPanel.SetActive(false);
                inforPanel.SetActive(false);
                mainPanel.SetActive(false);
                controlPanel.SetActive(false);
                pausePanel.SetActive(true);
                gameoverPanel.SetActive(false);
                break;
            case GameState.GameOver:
                settingPanel.SetActive(false);
                inforPanel.SetActive(false);
                mainPanel.SetActive(false);
                controlPanel.SetActive(false);
                pausePanel.SetActive(false);
                gameoverPanel.SetActive(true);
                break;
            default:
                break;
        }
    }

    #region Main State
    private void OnMainState () {
        mainCanvasGroup.DOFade(1f, 1f);
    }

    public void OnMain_PlayBtnClick () {

    }

    public void OnMain_InforBtnClick () {
        mainCanvasGroup.DOFade(0f, .5f).OnComplete(() => {
            inforPanel.transform.DOLocalMove(Vector2.zero, .5f);
        });
    }

    public void OnMain_SettingBtnClick () {
        mainCanvasGroup.DOFade(0f, .5f).OnComplete(() => {
            settingPanel.transform.DOLocalMove(Vector2.zero, .5f);
        });
    }

    public void OnMain_ExitBtnClick () {
        Debug.Log("Quit");
        Application.Quit();
    }
    #endregion
}