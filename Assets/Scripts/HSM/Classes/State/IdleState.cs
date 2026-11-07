using UnityEngine;

public class IdleState : AnimationState
{
    public override void StateUpdate()
    {
        Debug.Log($"{gameObject} is in update");
    }
}
