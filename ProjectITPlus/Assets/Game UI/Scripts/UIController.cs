using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Observer;
using System;
using Audio;

public class UIController : MonoBehaviour {
    [Header("Container")]
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject inforPanel;
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject controlPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] GameObject gamewinPanel; 
    [SerializeField] GameObject loadPanel;
    [SerializeField] GameObject background;

    [Header("Properties")]
    [SerializeField] CanvasGroup mainCanvasGroup;
    [SerializeField] GameObject loadImage;
    [SerializeField] Text txtTimerOnPlay;
    [SerializeField] Text txtTimerGameOver;
    [SerializeField] Text txtTimerBest;
    [SerializeField] GameObject sfxOff;
    [SerializeField] GameObject sfxOn;
    [SerializeField] GameObject musicOff;
    [SerializeField] GameObject musicOn;
    private Vector2 startSettingPos;
    private Vector2 startInforPos;
    private Vector2 startPausePos;
    private Vector2 startGameOverPos;

    private bool isFinishLoadAnimation;
    private bool isPlayMode;
    private bool isMenuMode;

    private bool activeTimer;
    private float currentTime;
    private TimeSpan time;

    private void Awake () {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

        startSettingPos = settingPanel.transform.localPosition;
        startInforPos = inforPanel.transform.localPosition;
        startPausePos = pausePanel.transform.localPosition;
        startGameOverPos = gameoverPanel.transform.localPosition;
        mainCanvasGroup.alpha = 0;
        isPlayMode = false;
        isMenuMode = false;
        activeTimer = false;
        currentTime = 0;

        bool muteSFX = PlayerPrefs.GetInt("SFX", 0) == 1;
        bool muteMusic = PlayerPrefs.GetInt("Music", 0) == 1;

        if (muteMusic) {
            musicOff.SetActive(true);
            musicOn.SetActive(false);
        }
        else {
            musicOff.SetActive(false);
            musicOn.SetActive(true);
        }
        if (muteSFX) {
            sfxOff.SetActive(true);
            sfxOn.SetActive(false);
        }
        else {
            sfxOff.SetActive(false);
            sfxOn.SetActive(true);
        }
        GameStateManager.GameStateChanged += GameStateChanged;

        this.RegisterListener(EventID.OnPlay, (param) => OnEventPlay());
        this.RegisterListener(EventID.PlayerDead, (param) => OnEventGameOver());
        this.RegisterListener(EventID.OnWin, (param) => OnEventGameWin());
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
                gamewinPanel.SetActive(false);
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
                gamewinPanel.SetActive(false);
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
                gamewinPanel.SetActive(false);
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
                gamewinPanel.SetActive(false);
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
                gamewinPanel.SetActive(false);
                OnGameOverState();
                break;
            case GameState.GameWin:
                settingPanel.SetActive(false);
                inforPanel.SetActive(false);
                mainPanel.SetActive(false);
                controlPanel.SetActive(false);
                pausePanel.SetActive(false);
                gameoverPanel.SetActive(false);
                loadPanel.SetActive(false);
                background.SetActive(false);
                gamewinPanel.SetActive(true);
                OnGameWinState();
                break;
            default:
                break;
        }
    }

    private void Update () {
        LoadLogicUpdate();
        TimerLogicUpdate();
    }

    private void OnEventPlay () {
        activeTimer = true;
        currentTime = 0f;
    }

    private void OnEventGameOver () {
        activeTimer = false;
        GameStateManager.CurrentState = GameState.GameOver;
    }

    private void OnEventGameWin () {
        activeTimer = false;
        GameStateManager.CurrentState = GameState.GameWin;
        float bestTime = PlayerPrefs.GetFloat("BestTime", currentTime);
        if (currentTime < bestTime) {
            PlayerPrefs.SetFloat("BestTime", currentTime);
        }
        time = TimeSpan.FromSeconds(bestTime);
        txtTimerBest.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
        time = TimeSpan.FromSeconds(currentTime);
        txtTimerGameOver.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    private void TimerLogicUpdate () {
        if (activeTimer) {
            currentTime += Time.deltaTime;
        }
        time = TimeSpan.FromSeconds(currentTime);
        txtTimerOnPlay.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    #region Main State
    private void OnMainState () {
        Time.timeScale = 1;
        mainCanvasGroup.DOFade(1f, 1f);
    }

    public void OnMain_PlayBtnClick () {
        AudioManager.Instance.PlaySFX("Select");
        GameStateManager.CurrentState = GameState.Load;
        isPlayMode = true;
        StartCoroutine(SceneHelper.DoLoadMap1());
    }

    public void OnMain_InforBtnClick () {
        AudioManager.Instance.PlaySFX("Select");
        mainCanvasGroup.DOFade(0f, .5f).OnComplete(() => {
            inforPanel.SetActive(true);
            mainPanel.SetActive(false);
            inforPanel.transform.DOLocalMove(Vector2.zero, .5f);
        });
    }

    public void OnMain_SettingBtnClick () {
        AudioManager.Instance.PlaySFX("Select");
        mainCanvasGroup.DOFade(0f, .5f).OnComplete(() => {
            settingPanel.SetActive(true);
            mainPanel.SetActive(false);
            settingPanel.transform.DOLocalMove(Vector2.zero, .5f);
        });
    }

    public void OnMain_ExitBtnClick () {
        AudioManager.Instance.PlaySFX("Select");
        Application.Quit();
    }

    public void OnInfor_CloseBtnClick () {
        AudioManager.Instance.PlaySFX("Select");
        inforPanel.transform.DOLocalMove(startInforPos, .5f).OnComplete(() => {
            inforPanel.SetActive(false);
            mainPanel.SetActive(true);
            mainCanvasGroup.DOFade(1f, .5f);
        });
    }

    public void OnSetting_CloseBtnClick () {
        AudioManager.Instance.PlaySFX("Select");
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

    public void OnSetting_MusicOnBtnClick () {
        AudioManager.Instance.PlaySFX("Select");
        musicOff.SetActive(true);
        musicOn.SetActive(false);
        AudioManager.Instance.MuteMusic();
    }

    public void OnSetting_MusicOffBtnClick () {
        AudioManager.Instance.PlaySFX("Select");
        musicOn.SetActive(true);
        musicOff.SetActive(false);
        AudioManager.Instance.MuteMusic();
    }

    public void OnSetting_SFXOnBtnClick () {
        AudioManager.Instance.PlaySFX("Select");
        sfxOff.SetActive(true);
        sfxOn.SetActive(false);
        AudioManager.Instance.MuteSFX();
    }

    public void OnSetting_SFXOffBtnClick () {
        AudioManager.Instance.PlaySFX("Select");
        sfxOn.SetActive(true);
        sfxOff.SetActive(false);
        AudioManager.Instance.MuteSFX();
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
        AudioManager.Instance.PlaySFX("Pause");
        GameStateManager.CurrentState = GameState.Pause;
    }
    #endregion

    #region Pause State
    private void OnPauseState () {
        Time.timeScale = 0;
        pausePanel.transform.DOLocalMove(Vector2.zero, .5f).SetUpdate(true);
    }

    public void OnPause_HomeBtnClick () {
        AudioManager.Instance.PlaySFX("Select");
        pausePanel.transform.DOLocalMove(startPausePos, .5f).SetUpdate(true).OnComplete(() => {
            GameStateManager.CurrentState = GameState.Load;
            isMenuMode = true;
            StartCoroutine(SceneHelper.UnloadScene());
        });
    }

    public void OnPause_PlayBtnClick () {
        AudioManager.Instance.PlaySFX("Unpause");
        pausePanel.transform.DOLocalMove(startPausePos, .5f).SetUpdate(true).OnComplete(() => {
            GameStateManager.CurrentState = GameState.Play;
        });
    }

    public void OnPause_SettingBtnClick () {
        AudioManager.Instance.PlaySFX("Select");
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
        AudioManager.Instance.PlaySFX("Select");
        gameoverPanel.transform.DOLocalMove(startGameOverPos, .5f).OnComplete(() => {
            GameStateManager.CurrentState = GameState.Load;
            isMenuMode = true;
            StartCoroutine(SceneHelper.UnloadScene());
        });
    }

    public void OnGameOver_PlayBtnClick () {
        AudioManager.Instance.PlaySFX("Select");
        gameoverPanel.transform.DOLocalMove(startGameOverPos, .5f).SetUpdate(true).OnComplete(() => {
            GameStateManager.CurrentState = GameState.Load;
            isPlayMode = true;
            StartCoroutine(SceneHelper.DoLoadMap1());
        });
    }
    #endregion

    #region Game Win State
    private void OnGameWinState () {
        Time.timeScale = 1;
        gamewinPanel.transform.DOLocalMove(Vector2.zero, .5f);
    }

    public void OnGameWin_HomeBtnClick () {
        AudioManager.Instance.PlaySFX("Select");
        gamewinPanel.transform.DOLocalMove(startGameOverPos, .5f).OnComplete(() => {
            GameStateManager.CurrentState = GameState.Load;
            isMenuMode = true;
            StartCoroutine(SceneHelper.UnloadScene());
        });
    }

    public void OnGameWin_PlayBtnClick () {
        AudioManager.Instance.PlaySFX("Select");
        gamewinPanel.transform.DOLocalMove(startGameOverPos, .5f).SetUpdate(true).OnComplete(() => {
            GameStateManager.CurrentState = GameState.Load;
            isPlayMode = true;
            StartCoroutine(SceneHelper.DoLoadMap1());
        });
    }
    #endregion
}