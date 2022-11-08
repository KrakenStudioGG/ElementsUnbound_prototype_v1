using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private InputControls inputControls;

#region Events
    public delegate void MoveInputEvent(Vector2 moveInput);
    public event MoveInputEvent moveEvent;
    
    public delegate void LookInputEvent(Vector2 lookInput);
    public static event LookInputEvent lookEvent;
    
    public delegate void FireInputEvent(float fireInput);
    public static event FireInputEvent fireEvent;

#endregion

#region Getters
    /*public Vector2 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }*/
#endregion

    private void Awake()
    {
        inputControls = new InputControls();
    }

    private void OnEnable()
    {
        inputControls.Player.Enable();
    }
    
    private void OnDisable()
    {
        inputControls.Player.Disable();
    }
    
    private void OnMove(InputValue value)
    {
        if(moveEvent != null)
            Debug.Log("OnMoveInput subbed");
        moveEvent?.Invoke(value.Get<Vector2>());
    }

    private void OnLook(InputValue value)
    {
        lookEvent?.Invoke(value.Get<Vector2>());
    }

    private void OnFire(InputValue value)
    {
        fireEvent?.Invoke(value.Get<float>());
    }

}
