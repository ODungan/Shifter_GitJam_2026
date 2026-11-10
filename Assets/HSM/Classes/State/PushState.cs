using UnityEngine;

public class PushState : BaseState
{
    private Rigidbody2D body;
    public bool pushed = false;
    private Vector2 direction;
    public override void StateEnter()
    {
        base.StateEnter();
        PlayerMachine machine = Machine as PlayerMachine;
        if (machine != null)
        {
            direction = machine.character.pushed;
            body = machine.character.body;
        }
        pushed = false;
    }
    public override void StateFixedUpdate()
    {
        if(direction.magnitude > Vector2.zero.magnitude && pushed == false)
        {
            body.AddForce(direction, ForceMode2D.Impulse);
            pushed = true;
        }
        if(body.linearVelocity.magnitude < 0.2 && isActive == true && pushed == true)
        {
            StateComplete();
        }
    }
}
