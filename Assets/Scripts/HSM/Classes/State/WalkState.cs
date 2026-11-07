using UnityEngine;
using UnityEngine.InputSystem;

public class WalkState : BaseState
{
    public Rigidbody2D body;
    public float maxSpeed;
    public float acceleration;
    public float deceleration;
    private float _accelTotal;
    private float _decelTotal;
    public Vector2 _direction;
    
    public override void Awake()
    {
        base.Awake();
        _accelTotal = (50 * acceleration) / maxSpeed;
        _decelTotal = (50 * deceleration) / maxSpeed;
    }
    public override void StateInit(StateMachine machine, BaseState parent = null)
    {
        base.StateInit(machine, parent);
        
    }
    public virtual void Move(Vector2 direction)
    {
        _direction = direction;
    }
    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        if(_direction == Vector2.zero && timeActive > 0.1f)
        {
            StateComplete();
            body.linearVelocity = _direction;
            return;
        } 
        float targetSpeedX = _direction.x * maxSpeed;
        float targetSpeedY = _direction.y * maxSpeed;

        float accelRateX = (Mathf.Abs(targetSpeedX) > 0.01f) ? _accelTotal : _decelTotal;
        float accelRateY = (Mathf.Abs(targetSpeedY) > 0.01f) ? _accelTotal : _decelTotal;

        float speedDiffX = targetSpeedX - body.linearVelocityX;
        float speedDiffY = targetSpeedY - body.linearVelocityY;
        Debug.Log($"accelRate {accelRateX}, speedDiff {speedDiffX}");
        float movementX = speedDiffX * accelRateX;
        float movementY = speedDiffY * accelRateY;
        Vector2 force = new Vector2(movementX, movementY);
        Debug.Log($"{body} has direction of {_direction}, and applied force of {force}"); //NOT WORKING!!
        body.AddForce(force);
    }
    public override void StateExit()
    {
        _direction = Vector2.zero;
        body.linearVelocity = _direction;
        base.StateExit();
    }
}
