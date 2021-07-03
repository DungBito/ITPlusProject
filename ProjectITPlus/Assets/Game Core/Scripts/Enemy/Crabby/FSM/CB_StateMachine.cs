using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CB_StateMachine {
    public CB_State CurrentState { get; private set; }

    public void Initialize(CB_State startingState) {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(CB_State newState) {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
