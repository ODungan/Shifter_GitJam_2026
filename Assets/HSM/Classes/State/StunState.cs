using UnityEngine;

public class StunState : BaseState
{
    public float duration;
    private float time;

    public override void StateEnter()
    {
        base.StateEnter();
        Debug.Log("Stunned");
        time = 0;
    }
    public override void StateUpdate()
    {
        base.StateUpdate();
        time += Time.deltaTime;
        if(time > duration)
        {
            StateComplete();
        }
    }
}
