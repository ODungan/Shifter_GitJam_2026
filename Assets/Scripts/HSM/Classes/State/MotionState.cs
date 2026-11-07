using UnityEngine;
using UnityEngine.InputSystem;

public class MotionState : InputState
{
    public Vector2 Direction;
    public override void Move(InputAction direction)
    {
        Direction = direction.ReadValue<Vector2>();
        if(ActiveChild != null)
        {
            WalkState move = ActiveChild as WalkState;
            if(move != null)
            {move.Move(Direction);}
        }
    }
    public override void StateExit()
    {
        if(ActiveChild != null)
        {
            WalkState move = ActiveChild as WalkState;
            move.Move(Vector2.zero);
        }
        base.StateExit();
    }
}
