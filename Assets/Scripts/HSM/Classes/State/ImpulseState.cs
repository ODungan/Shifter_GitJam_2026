using UnityEngine;

public class ImpulseState : BaseState
{
    public Rigidbody2D body;
    public Vector2 force;
    public bool pushed = false;
    private Vector2 direction;
    public override void StateEnter()
    {
        base.StateEnter();
        pushed = false;
    }
    public override void StateFixedUpdate()
    {
        DashState parent = Parent as DashState;
        if (parent != null)
        {
            direction = parent.direction;
        }
        if(direction.magnitude > Vector2.zero.magnitude && pushed == false)
        {
            body.AddForce(direction * force, ForceMode2D.Impulse);
            pushed = true;
        }
        if(body.linearVelocity.magnitude < 0.2 && isActive == true && pushed == true)
        {
            StateComplete();
        }
    }
}
