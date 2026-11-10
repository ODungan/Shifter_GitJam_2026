using UnityEngine;

public class FollowState : BaseState
{
    public float speed;
    private BaseCharacter character;
    public override void StateEnter()
    {
        base.StateEnter();
        PlayerMachine machine = Machine as PlayerMachine;
        if(machine != null)
        {
            character = machine.character;
        }
    }
    public override void StateFixedUpdate()
    {
        base.StateFixedUpdate();
        Vector2 direction =  character.follow.transform.position - character.gameObject.transform.position;
        if(character.follow.activeSelf == true)
        {
            character.body.AddForce(direction * speed);
        }
        else
        {
            StateComplete();
        }
    }
}
