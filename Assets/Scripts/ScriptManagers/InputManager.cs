using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;


[Serializable]
public class PlayerMoveEvent : UnityEvent<Vector2>{}
[Serializable]
public class PlayerLookEvent : UnityEvent<Vector2>{}
[Serializable]
public class OnFireEvent : UnityEvent<bool>{}
[Serializable]
public class CastSpellEvent : UnityEvent<Vector2, Vector2>{}

public class InputManager : Singleton<InputManager>
{
    public InputControls inputControls;
    public PlayerMoveEvent moveEvent;
    public PlayerLookEvent lookEvent;
    public OnFireEvent fireEvent;
    private bool _wasPressedThisFrame;

    private void OnEnable()
    {
        inputControls = new InputControls();
        inputControls.Player.Enable();
        inputControls.Player.Move.performed += OnMove;
        inputControls.Player.Move.canceled += OnMove;
        inputControls.Player.Look.performed += OnLook;
        inputControls.Player.Look.canceled += OnLook;
        _wasPressedThisFrame = inputControls.Player.Fire.WasPressedThisFrame();
        inputControls.Player.Fire.canceled += OnFire;
    }
    
    private void OnDisable()
    {
        inputControls.Player.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 moveInput = ctx.ReadValue<Vector2>();
        moveEvent?.Invoke(moveInput);
    }

    private void OnLook(InputAction.CallbackContext ctx)
    {
        Vector2 lookInput = ctx.ReadValue<Vector2>();
        lookEvent?.Invoke(lookInput);
    }

    private void OnFire(InputAction.CallbackContext ctx)
    {
        fireEvent?.Invoke(ctx.ReadValue<bool>());
    }
    
    public void OnSpellSelect(InputValue value)
    {
        //onSelectEvent?.Invoke(value.Get<KeyCode>());
    }
    
}
