using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_StateMachine {
    public PS_State CurrentState { get; private set; }

    public void Initialize(PS_State startingState) {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PS_State newState) {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
