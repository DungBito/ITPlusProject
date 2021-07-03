using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_InputHandle : MonoBehaviour {
    #region Input
    public int XInput { get; private set; }

    public bool JumpInput { get; private set; }

    public bool ThrowInput { get; private set; }
    public bool ThrowInputStop { get; private set; }

    public bool SkillInput { get; private set; }
    #endregion

    #region Variables, Update
    private float inputHoldTime = .1f;
    private float jumpInputStart;
    private float throwInputStart;
    private float skillInputStart;

    private void Update() {
        CheckJumpInputHold();
        CheckThrowInputHold();
        CheckSkillInputHold();
    }
    #endregion

    #region Input functions
    //Move Input
    public void OnMoveInput(InputAction.CallbackContext context) {
        XInput = (int)context.ReadValue<float>();
    }

    //Jump Input
    public void OnJumpInput(InputAction.CallbackContext context) {
        if (context.started) {
            JumpInput = true;
            jumpInputStart = Time.time;
        }
    }
    public void UseJumpInput() {
        JumpInput = false;
    }
    private void CheckJumpInputHold() {
        if (Time.time >= jumpInputStart + inputHoldTime) {
            JumpInput = false;
        }
    }

    //Throw Input
    public void OnThrowInput(InputAction.CallbackContext context) {
        if (context.started) {
            ThrowInput = true;
            ThrowInputStop = false;
            throwInputStart = Time.time;
        }

        if (context.canceled) {
            ThrowInputStop = true;
        }
    }
    public void UseThrowInput() {
        ThrowInput = false;
    }
    private void CheckThrowInputHold() {
        if (Time.time >= throwInputStart + inputHoldTime) {
            ThrowInput = false;
        }
    }

    //Skill Input
    public void OnSkillInput(InputAction.CallbackContext context) {
        if (context.started) {
            SkillInput = true;
            skillInputStart = Time.time;
        }
    }
    public void UseSkillInput() {
        SkillInput = false;
    }
    private void CheckSkillInputHold() {
        if (Time.time >= skillInputStart + inputHoldTime) {
            SkillInput = false;
        }
    }
    #endregion
}
