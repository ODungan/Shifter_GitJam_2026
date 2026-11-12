using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputListener : MonoBehaviour
{
    public event Action OnLeftTrigger;
    public event Action OnRightTrigger;
    public event Action OnWest;
    public event Action OnNorth;
    public event Action OnEast;
    public event Action OnSouth;
    public event Action<InputAction> OnMove;
    public InputSystem_Actions _player;
    public InputAction _movement;

    private void Awake()
    {
        _player = new InputSystem_Actions();
    }
    private void OnEnable()
    {
        _player.Combat.Enable();
        _player.Combat.LeftTrigger.performed += LeftTrigger;
        _player.Combat.RightTrigger.performed += RightTrigger;
        _player.Combat.West.performed += West;
        _player.Combat.North.performed += North;
        _player.Combat.East.performed += East;
        _player.Combat.South.performed += South;
        _player.Combat.South.canceled += South;
        _player.Combat.Move.performed += Move;
        _player.Combat.Move.canceled += Move;

        _movement = _player.Combat.Move;
    }
    private void FixedUpdate() 
    {
        if(_player.Combat.Move.IsPressed())
        {
            OnMove?.Invoke(_movement);    
        }
    }
    public void LeftTrigger(InputAction.CallbackContext context)
    {
        OnLeftTrigger?.Invoke();
    }
    public void RightTrigger(InputAction.CallbackContext context)
    {
        OnRightTrigger?.Invoke();
    }
    public void West(InputAction.CallbackContext context)
    {
        OnWest?.Invoke();
    }
    public void North(InputAction.CallbackContext context)
    {
        OnNorth?.Invoke();
    }
    public void East(InputAction.CallbackContext context)
    {
        OnEast?.Invoke();
    }
    public void South(InputAction.CallbackContext context)
    {
        OnSouth?.Invoke();
    }
    public void Move(InputAction.CallbackContext context)
    {
        OnMove?.Invoke(_movement);
    }

    public void OnDisable()
    {
        _player.Combat.LeftTrigger.performed -= LeftTrigger;
        _player.Combat.RightTrigger.performed -= RightTrigger;
        _player.Combat.West.performed -= West;
        _player.Combat.North.performed -= North;
        _player.Combat.East.performed -= East;
        _player.Combat.South.performed -= South;
        _player.Combat.South.canceled -= South;
        _player.Combat.Move.performed -= Move;
        _player.Combat.Move.canceled -= Move;
        _player.Combat.Disable();
    }
}
