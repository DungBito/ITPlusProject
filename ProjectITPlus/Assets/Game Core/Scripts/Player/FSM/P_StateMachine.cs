using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_StateMachine {
    public P_State CurrentState { get; private set; }

    public void Initialize(P_State startingState) {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(P_State newState) {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
