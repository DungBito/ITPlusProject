using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Observer;
using System;

public class UIController : MonoBehaviour {
    [Header("Container")]
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject inforPanel;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] GameObject loadPanel;
    [SerializeField] GameObject background;

    [Header("Properties")]
    [SerializeField] CanvasGroup mainCanvasGroup;
    [SerializeField] GameObject loadImage;
    [SerializeField] Text txtTimerOnPlay;
    [SerializeField] Text txtTimerOnGameOver;
    private Vector2 startSettingPos;
    private Vector2 startInforPos;
    private Vector2 startPausePos;
    private Vector2 startGameOverPos;
    private bool isFinishLoadAnimation;
    private bool isPlayMode;
    private bool isMenuMode;
    private float currentTime;
    private bool timerActive;

    private void Awake () {
        startSettingPos = settingPanel.transform.localPosition;
        startInforPos = inforPanel.transform.localPosition;
        startPausePos = pausePanel.transform.localPosition;
        startGameOverPos = gameoverPanel.transform.localPosition;
        isFinishLoadAnimation = false;
        isPlayMode = false;
        isMenuMode = false;
        currentTime = 0f;
        timerActive = false;
        mainCanvasGroup.alpha = 0;

        this.RegisterListener(EventID.OnPlay, (param) => OnEventPlay());
        this.RegisterListener(EventID.PlayerDead, (param) => OnEventPlayerDead());
    }

    private void Start () {
        GameStateManager.GameStateChanged += GameStateChanged;
    }

    private void Update () {
        LoadLogicUpdate();
        TimerLogicUpdate();
    }

    private void GameStateChanged () {
        switch (GameStateManager.CurrentState) {
            case GameState.None:
                break;
            case GameState.Main:
                settingPanel.SetActive(false);
                inforPanel.SetActive(false);
                mainPanel.SetActive(true);
                controlPanel.SetActive(false);
                pausePanel.SetActive(false);
                gameoverPanel.SetActive(false);
                loadPanel.SetActive(false);
                background.SetActive(true);
                OnMainState();
                break;
            case GameState.Play:
                settingPanel.SetActive(false);
                inforPanel.SetActive(false);
                mainPanel.SetActive(false);
                controlPanel.SetActive(true);
                pausePanel.SetActive(false);
                gameoverPanel.SetActive(false);
                loadPanel.SetActive(false);
                background.SetActive(false);
                OnPlayState();
                break;
            case GameState.Load:
                settingPanel.SetActive(false);
                inforPanel.SetActive(false);
                mainPanel.SetActive(false);
                controlPanel.SetActive(false);
                pausePanel.SetActive(false);
                gameoverPanel.SetActive(false);
                loadPanel.SetActive(true);
                background.SetActive(true);
                OnLoadState();
                break;
            case GameState.Pause:
                settingPanel.SetActive(false);
                inforPanel.SetActive(false);
                mainPanel.SetActive(false);
                controlPanel.SetActive(false);
                pausePanel.SetActive(true);
                gameoverPanel.SetActive(false);
                loadPanel.SetActive(false);
                background.SetActive(false);
                OnPauseState();
                break;
            case GameState.GameOver:
                settingPanel.SetActive(false);
                inforPanel.SetActive(false);
                mainPanel.SetActive(false);
                controlPanel.SetActive(false);
                pausePanel.SetActive(false);
                gameoverPanel.SetActive(true);
                loadPanel.SetActive(false);
                background.SetActive(false);
                OnGameOverState();
                break;
            default:
                break;
        }
    }

    private void OnEventPlay () {
        timerActive = true;
        currentTime = 0f;
    }

    private void OnEventPlayerDead () {
        timerActive = false;
        GameStateManager.CurrentState = GameState.GameOver;
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        txtTimerOnGameOver.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    private void TimerLogicUpdate () {
        if (timerActive) {
            currentTime += Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        txtTimerOnPlay.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    #region Main State
    private void OnMainState () {
        Time.timeScale = 1;
        mainCanvasGroup.DOFade(1f, 1f);
    }

    public void OnMain_PlayBtnClick () {
        GameStateManager.CurrentState = GameState.Load;
        isPlayMode = true;
        StartCoroutine(SceneHelper.DoLoadMap1());
    }

    public void OnMain_InforBtnClick () {
        mainCanvasGroup.DOFade(0f, .5f).OnComplete(() => {
            inforPanel.SetActive(true);
            mainPanel.SetActive(false);
            inforPanel.transform.DOLocalMove(Vector2.zero, .5f);
        });
    }

    public void OnMain_SettingBtnClick () {
        mainCanvasGroup.DOFade(0f, .5f).OnComplete(() => {
            settingPanel.SetActive(true);
            mainPanel.SetActive(false);
            settingPanel.transform.DOLocalMove(Vector2.zero, .5f);
        });
    }

    public void OnMain_ExitBtnClick () {
        Application.Quit();
    }

    public void OnInfor_CloseBtnClick () {
        inforPanel.transform.DOLocalMove(startInforPos, .5f).OnComplete(() => {
            inforPanel.SetActive(false);
            mainPanel.SetActive(true);
            mainCanvasGroup.DOFade(1f, .5f);
        });
    }

    public void OnSetting_CloseBtnClick () {
        settingPanel.transform.DOLocalMove(startSettingPos, .5f).OnComplete(() => {
            if (GameStateManager.CurrentState == GameState.Main) {
                settingPanel.SetActive(false);
                mainPanel.SetActive(true);
                mainCanvasGroup.DOFade(1f, .5f);
            }
            else if (GameStateManager.CurrentState == GameState.Pause) {
                settingPanel.SetActive(false);
                pausePanel.SetActive(true);
                pausePanel.transform.DOLocalMove(Vector2.zero, .5f).SetUpdate(true);
            }
        }).SetUpdate(true);
    }

    public void OnSetting_MusicBtnClick () {
        Debug.Log("Music");
    }

    public void OnSetting_SFXBtnClick () {
        Debug.Log("SFX");
    }
    #endregion

    #region Load State
    private void OnLoadState () {
        Time.timeScale = 0;
        loadImage.transform.DOScale(1, .5f).OnComplete(() => {
            isFinishLoadAnimation = true;
        }).SetUpdate(true);
    }

    private void LoadLogicUpdate () {
        if (GameStateManager.CurrentState == GameState.Load && isFinishLoadAnimation) {
            isFinishLoadAnimation = false;
            if (isPlayMode) {
                background.SetActive(false);
            }
            else {
                background.SetActive(true);
            }
            loadImage.transform.DOScale(0, .5f).OnComplete(() => {
                if (isPlayMode) {
                    isPlayMode = false;
                    GameStateManager.CurrentState = GameState.Play;
                }
                else if (isMenuMode) {
                    isMenuMode = false;
                    GameStateManager.CurrentState = GameState.Main;
                }
            }).SetUpdate(true);
        }
    }
    #endregion

    #region Play State
    private void OnPlayState () {
        Time.timeScale = 1;
    }

    public void OnPlay_PauseBtnClick () {
        GameStateManager.CurrentState = GameState.Pause;
    }
    #endregion

    #region Pause State
    private void OnPauseState () {
        Time.timeScale = 0;
        pausePanel.transform.DOLocalMove(Vector2.zero, .5f).SetUpdate(true);
    }

    public void OnPause_HomeBtnClick () {
        pausePanel.transform.DOLocalMove(startPausePos, .5f).SetUpdate(true).OnComplete(() => {
            GameStateManager.CurrentState = GameState.Load;
            isMenuMode = true;
            StartCoroutine(SceneHelper.UnloadScene());
        });
    }

    public void OnPause_PlayBtnClick () {
        pausePanel.transform.DOLocalMove(startPausePos, .5f).SetUpdate(true).OnComplete(() => {
            GameStateManager.CurrentState = GameState.Play;
        });
    }

    public void OnPause_SettingBtnClick () {
        pausePanel.transform.DOLocalMove(startPausePos, .5f).SetUpdate(true).OnComplete(() => {
            pausePanel.SetActive(false);
            settingPanel.SetActive(true);
            settingPanel.transform.DOLocalMove(Vector2.zero, .5f).SetUpdate(true);
        });
    }
    #endregion

    #region Game Over State
    private void OnGameOverState () {
        Time.timeScale = 1;
        gameoverPanel.transform.DOLocalMove(Vector2.zero, .5f);
    }

    public void OnGameOver_HomeBtnClick () {
        gameoverPanel.transform.DOLocalMove(startGameOverPos, .5f).OnComplete(() => {
            GameStateManager.CurrentState = GameState.Load;
            isMenuMode = true;
            StartCoroutine(SceneHelper.UnloadScene());
        });
    }

    public void OnGameOver_PlayBtnClick () {
        gameoverPanel.transform.DOLocalMove(startGameOverPos, .5f).SetUpdate(true).OnComplete(() => {
            GameStateManager.CurrentState = GameState.Load;
            isPlayMode = true;
            StartCoroutine(SceneHelper.DoLoadMap1());
        });
    }
    #endregion
}