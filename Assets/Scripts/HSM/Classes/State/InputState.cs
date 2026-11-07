using UnityEngine;
using UnityEngine.InputSystem;
using System;
public class InputState : BaseState, iPlayable
{
    /// <summary>
    /// Input State, Handles all input requests
    /// </summary>
    public InputListener Input;
    public BaseState LTrigState;
    public BaseState RTrigState;
    public BaseState EastState;
    public BaseState NorthState;
    public BaseState WestState;
    public BaseState SouthState;
    public BaseState MoveState;
    public event Action<Vector2> OnMoveInput;
    #region Input Methods
    public virtual void West()
    {
        if(ActiveChild != null)
        {
            InputState input = ActiveChild as InputState;
            if(input != null)
            {
                input.West();
                return;
            }
        }
        if(WestState == null){return;}
        Transition.RequestStateChange(WestState,StateLeaf());
    }
    public virtual void North()
    {
        Debug.Log($"North! triggered from: {gameObject}");
        if(ActiveChild != null)
        {
            InputState input = ActiveChild as InputState;
            if(input != null)
            {
                Debug.Log($"North! from {gameObject} active child: {input}");
                input.North();
                return;
            }
        }
        if(NorthState == null){return;}
        Debug.Log($"North! state {NorthState} transitioned to from {gameObject}");
        Transition.RequestStateChange(NorthState,StateLeaf());
    }
    public virtual void East()
    {
        if(ActiveChild != null)
        {
            InputState input = ActiveChild as InputState;
            if(input != null)
            {
                input.East();
                return;
            }
        }
        if(EastState == null){return;}
        Transition.RequestStateChange(EastState,StateLeaf());
    }
    public virtual void South()
    {
        if(ActiveChild != null)
        {
            InputState input = ActiveChild as InputState;
            if(input != null)
            {
                input.South();
                return;
            }
        }
        if(SouthState == null){return;}
        Transition.RequestStateChange(SouthState,StateLeaf());
    }
    public virtual void Move(InputAction direction)
    {
        if(Parent != null)
        {
            InputState parent = Parent as InputState;
            if(parent == null)
            {
                OnMoveInput?.Invoke(direction.ReadValue<Vector2>());
            }
        }
        
        if(ActiveChild != null)
        {
            InputState input = ActiveChild as InputState;
            if(input != null){input.Move(direction);return;}
        }
        if(MoveState == null){return;}
        Transition.RequestStateChange(MoveState,StateLeaf());
    }
    public virtual void LeftTrigger()
    {
        Debug.Log($"Left Trigger triggered from: {gameObject}");
        if(ActiveChild != null)
        {
            InputState input = ActiveChild as InputState;
            if(input != null)
            {
                input.LeftTrigger();
                return;
            }
        }
        if(LTrigState == null){return;}
        Transition.RequestStateChange(LTrigState,StateLeaf());
    }
    public virtual void RightTrigger()
    {
        if(ActiveChild != null)
        {
            InputState input = ActiveChild as InputState;
            if(input != null)
            {
                input.RightTrigger();
                return;
            }
        }
        if(RTrigState == null){return;}
        Transition.RequestStateChange(RTrigState,StateLeaf());
    }
    #endregion
    public override void StateEnter()
    {
        base.StateEnter();
        InputState parent = Parent as InputState;
        if(parent == null)
        {
            InputSubscribe();
        }
    }
    public override void ChildStateComplete()
    {
        if(ActiveChild != null){ActiveChild.StateExit();}
        if(ActiveChild == Default)
        {
            StateComplete();
            return;
        }
        ActiveChild = null;
        StateDefault();
    }
    public override void StateExit()
    {
        base.StateExit();
        InputUnsubscribe();
    }
    public void InputSubscribe() // subscribe L and R Triggers
    {
        Input.OnLeftTrigger += LeftTrigger;
        Input.OnRightTrigger += RightTrigger;
        Input.OnEast += East;
        Input.OnWest += West;
        Input.OnNorth += North;
        Input.OnSouth += South;
        Input.OnMove += Move;
    }
    public void InputUnsubscribe()
    {
        Input.OnLeftTrigger -= LeftTrigger;
        Input.OnRightTrigger -= RightTrigger;
        Input.OnEast -= East;
        Input.OnWest -= West;
        Input.OnNorth -= North;
        Input.OnSouth -= South;
        Input.OnMove -= Move;
    }
    public void OnDisable()
    {
        InputUnsubscribe();
    }
}
