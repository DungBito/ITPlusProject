using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStateManager : MonoBehaviour {
    public static GameStateManager Instance { get; private set; }
    public static Action GameStateChanged;
    public static GameState CurrentState {
        get {
            return Instance.currentState;
        }

        set {
            if (Instance.currentState != value) {
                Instance.lastState = Instance.currentState;
                Instance.currentState = value;
                if (GameStateChanged != null) {
                    GameStateChanged.Invoke();
                }
            }
        }
    }
    public static GameState LastState {
        get => Instance.lastState;
        set => Instance.lastState = value;
    }

    [SerializeField] GameState currentState = GameState.None;
    [SerializeField] GameState lastState = GameState.None;

    private void Awake () {
        Instance = this;
    }

    private void Start () {
        CurrentState = GameState.Main;
    }
}

public enum GameState {
    None,
    Main,
    Play,
    Pause,
    GameOver
}
