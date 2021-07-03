using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CT_StateMachine {
    public CT_State CurrentState { get; private set; }

    public void Initialize(CT_State startingState) {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(CT_State newState) {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
