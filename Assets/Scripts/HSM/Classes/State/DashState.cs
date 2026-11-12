using UnityEngine;

public class DashState : InputState
{
    public Vector2 direction;
    public int cost;
    public override void StateInit(StateMachine machine, BaseState parent = null)
    {
        base.StateInit(machine, parent);
    }
    public override void LeftTrigger()
    {
        Debug.Log("This was the source of the bug!");
    }
}
