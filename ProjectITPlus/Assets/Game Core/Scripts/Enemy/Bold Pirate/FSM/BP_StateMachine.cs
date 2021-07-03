using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BP_StateMachine {
    public BP_State CurrentState { get; private set; }

    public void Initialize(BP_State startingState) {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(BP_State newState) {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
