using UnityEngine;

public class DashState : InputState
{
    public Vector2 direction;
    public int cost;
    public override void StateInit(StateMachine machine, BaseState parent = null)
    {
        base.StateInit(machine, parent);
        PlayerMachine mac = Machine as PlayerMachine;
        if(mac != null)
        {
            direction = mac.input._movement.ReadValue<Vector2>();
            float res = mac.character.resolve;
            if(res > cost)
            {
                mac.character.Resolve(cost, true);
                Debug.Log($"Resolve = {res}");
            }
            else
            {
                Debug.Log($"Not enough resolve: {res}");
                StateComplete();
            }
        }
    }
    public override void LeftTrigger()
    {
        Debug.Log("This was the source of the bug!");
    }
}
